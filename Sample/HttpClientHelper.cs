using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Sample
{
    public class HttpClientHelper
    {
        private HttpClient _client;

        public HttpClientHelper(HttpClientParameters httpClientParameters)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
                                                   | SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12;
            HttpClientParameters = httpClientParameters;
        }

        public string Result { get; private set; }

        public bool Success { get; private set; }

        public HttpClientParameters HttpClientParameters { get; }


        public async Task<bool> CallService()
        {
            if (HttpClientParameters.EndPoint.IsNullOrBlank())
            {
                throw new Exception("ServiceURL Cannot be blank");
            }

            if (HttpClientParameters.Method.IsNullOrBlank())
            {
                throw new Exception("Method Cannot be blank");
            }

            _client = new HttpClient
            {
                BaseAddress = new Uri(HttpClientParameters.EndPoint),
                // Set timeout to infinite because Edge API service has its own timeout.
                Timeout = Timeout.InfiniteTimeSpan
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(HttpClientParameters.EndPoint),
                Method = GetVerb(HttpClientParameters.Method),
                Content = HttpClientParameters.Method == "GET"
                    ? null
                    : new StringContent(HttpClientParameters.Body, HttpClientParameters.EncodingType,
                        HttpClientParameters.ContentType)
            };

            AddHeadersToRequest(HttpClientParameters.Headers, request);

            var response = await _client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                Result = content;
                Success = true;
                return Success;
            }

            Result =
                $"Service Call failed with '{(int) response.StatusCode}' '{response.ReasonPhrase}'  '{response.Content.ReadAsStringAsync().Result}'.";
            Success = false;
            Console.WriteLine(Result);
            throw new Exception(Result);
        }


        private void AddHeadersToRequest(NameValueCollection headers, HttpRequestMessage request)
        {
            foreach (var key in headers.AllKeys)
            {
                request.Headers.Add(key, headers[key]);
            }
        }

        private HttpMethod GetVerb(string Method)
        {
            var httpMethod = HttpMethod.Get;
            Method = Method.ToUpper();
            switch (Method)
            {
                case "GET":
                    httpMethod = HttpMethod.Get;
                    break;
                case "POST":
                    httpMethod = HttpMethod.Post;
                    break;
                case "PUT":
                    httpMethod = HttpMethod.Put;
                    break;
                case "PATCH":
                    httpMethod = new HttpMethod("PATCH");
                    break;
                case "DELETE":
                    httpMethod = HttpMethod.Delete;
                    break;
            }

            return httpMethod;
        }
    }
}