using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    public class EdgeApi
    {
        public static async Task<string> CallApi(string accessToken)
        {
            var headers = new NameValueCollection();

            headers.Add("Authorization", Util.BuildAuthorizationHeader(accessToken));
            headers.Add("Accept", "application/json");

            var httpClientParameters = new HttpClientParameters
            {
                EndPoint = Portal.ServiceURL,
                Method = WebRequestMethods.Http.Post,
                Body = Util.ReadJsonFromFile(),
                EncodingType = Encoding.UTF8,
                Headers = headers,
                ContentType = "application/json"
            };

            var httpClientHelper = new HttpClientHelper(httpClientParameters);

            await httpClientHelper.CallService();

            var result = httpClientHelper.Result;
            Console.WriteLine(result);
            httpClientHelper = null;
            return result;
        }
    }
}