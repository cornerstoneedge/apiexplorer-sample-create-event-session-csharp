using System;
using System.Configuration;

namespace Sample
{
    public static class Portal
    {
        public static string Domain => GetDomain();
        public static string ClientId => ConfigurationManager.AppSettings["ClientId"];
        public static string ClientSecret => ConfigurationManager.AppSettings["ClientSecret"];
        public static string GrantType => ConfigurationManager.AppSettings["GrantType"];
        public static string Scope => ConfigurationManager.AppSettings["Scope"];
        public static string OAuth2URL => "/services/api/oauth2/token";
        public static string ServiceURL => ConfigurationManager.AppSettings["ServiceURL"];

        private static string GetDomain()
        {
            var uri = new Uri(ServiceURL);
            return uri.Host;
        }
    }
}