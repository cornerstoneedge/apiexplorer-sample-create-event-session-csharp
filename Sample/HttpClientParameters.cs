using System.Collections.Specialized;
using System.Text;

namespace Sample
{
    public class HttpClientParameters
    {
        public NameValueCollection Headers { get; set; } = new NameValueCollection();
        public string Body { get; set; }
        public string EndPoint { get; set; }
        public string ContentType { get; set; }
        public string Accept { get; set; }
        public string Method { get; set; }
        public Encoding EncodingType { get; set; } = Encoding.UTF8;
    }
}