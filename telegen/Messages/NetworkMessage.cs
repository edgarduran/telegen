using System;

namespace telegen.Messages
{
    public abstract class NetworkMessage : MsgBase
    {
        protected NetworkMessage(string uri) => Uri = new Uri(uri);
        public Uri Uri { get; }
        public string Address => Uri.ToString();
        public string Protocol => Uri.Scheme;
        public string Host => Uri.Host;
        public int Port => Uri.Port;
        public string Path => Uri.AbsolutePath;
        public string Query => Uri.Query;
    }

}
