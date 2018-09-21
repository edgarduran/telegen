using System;
using telegen.Messages;

namespace telegen.Messages
{
    public class WebResp : Operation
    {
        public WebResp(WebReq req, string response, DateTime utcTimeStamp, string clientName, int clientPort)
        {
            Request = req;
            Response = response;
            UtcTimeStamp = utcTimeStamp;
            ClientName = clientName;
            ClientPort = clientPort;
        }
        public WebReq Request { get; }
        public DateTime UtcTimeStamp { get; }
        public string ClientName { get; }
        public int ClientPort { get; }
        public string Response { get; }
    }

}
