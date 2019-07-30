using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Sample
{
    public static class Util
    {
        public static Dictionary<string, string> BuildRequestParameters()
        {
            try
            {
                var sb = new StringBuilder();
                var dictionary = new Dictionary<string, string>();
                dictionary.Add("clientId", Portal.ClientId);
                dictionary.Add("clientSecret", Portal.ClientSecret);
                dictionary.Add("grantType", Portal.GrantType);
                dictionary.Add("scope", Portal.Scope);

                return dictionary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string CreateHttpRequestDataJSON(Dictionary<string, string> dictionary)
        {
            var _sbParameters = new StringBuilder();
            var doublequotes = '"';
            var max = 0;
            _sbParameters.AppendLine("{");
            foreach (var param in dictionary.Keys)
            {
                _sbParameters.Append(doublequotes + param + doublequotes); //key => parameter name 
                _sbParameters.Append(':');
                _sbParameters.Append(doublequotes + dictionary[param] + doublequotes); //key value                
                if (max < dictionary.Keys.Count - 1)
                {
                    _sbParameters.Append(",");
                }

                _sbParameters.AppendLine("");
                max++;
            }

            _sbParameters.AppendLine("}");
            return _sbParameters.ToString();
        }

        public static string ReadJsonFromFile()
        {
            var CourseType = ConfigurationManager.AppSettings["CourseType"];
            var FilePath = string.Empty;

            if (CourseType.ToLower() == "event")
            {
                FilePath = ConfigurationManager.AppSettings["JsonFilePath"] + CourseType + ".json";
            }
            else if (CourseType.ToLower() == "session")
            {
                FilePath = ConfigurationManager.AppSettings["JsonFilePath"] + CourseType + ".json";
            }

            var sr = new StreamReader(FilePath);
            var JsonString = sr.ReadToEnd();
            return JsonString;
        }

        public static string BuildAuthorizationHeader(string accesstoken)
        {
            var sb = new StringBuilder();
            sb.Append("Bearer " + accesstoken);

            return sb.ToString();
        }

        public static bool Validate()
        {
            if (Portal.ClientId.IsNullOrBlank())
            {
                throw new Exception("ClientId Cannot be blank");
            }

            if (Portal.ClientSecret.IsNullOrBlank())
            {
                throw new Exception("ClientSecret Cannot be blank");
            }

            if (Portal.Scope.IsNullOrBlank())
            {
                throw new Exception("Scope Cannot be blank. It should be 'all' ");
            }

            if (Portal.GrantType.IsNullOrBlank())
            {
                throw new Exception("GrantType Cannot be blank. It should be'client_credentials' ");
            }

            if (Portal.OAuth2URL.IsNullOrBlank() || Portal.ServiceURL.IsNullOrBlank())
            {
                throw new Exception("OAuth2URL and ServiceURL Cannot be blank");
            }


            return true;
        }
    }
}