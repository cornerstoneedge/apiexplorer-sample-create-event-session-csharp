using System;

namespace Sample
{
    /// <summary>
    ///     Generating the Bearer Token from OAuth Token Implementation and calling Employee API using Bearer Token.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            Util.Validate();

            var accesstoken = OAuth2.GetAccessToken().Result;

            var result = EdgeApi.CallApi(accesstoken).Result;

            Console.WriteLine();
            Console.WriteLine("Enter To Exit");
            Console.ReadLine();
        }
    }
}