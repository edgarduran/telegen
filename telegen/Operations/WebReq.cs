using System;

namespace telegen.Operations
{
    public class WebReq
    {
        public Uri Uri { get; }
        public int Port { get; }
        public WebReq(string uri, int port = 80)
        {
            Uri = new Uri(uri);
            Port = port;
        }

        public override string ToString() => msg
            .Replace("{uri}", Uri.ToString())
            .Replace("{host}", Uri.Host)
            ;

        string msg = @"GET {uri} HTTP/1.1
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134
Accept-Language: en-US,en;q=0.5
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8
Upgrade-Insecure-Requests: 1
Accept-Encoding: gzip, deflate
Host: {host}
Connection: Keep-Alive

";
    }

}
