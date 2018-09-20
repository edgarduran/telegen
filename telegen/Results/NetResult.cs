using System;
using System.Collections.Generic;
using System.Diagnostics;
using telegen.Operations;

namespace telegen.Results
{
    public class NetResult : Result
    {
        public NetResult(WebResp resp)
        {
            LoadWebResp(resp);
        }

        public NetResult(WebResp resp, Process p) : base(p)
        {
            LoadWebResp(resp);
        }

        public NetResult(WebResp resp, string processName, DateTime utcStart, int procId) : base(processName, utcStart, procId)
        {
            LoadWebResp(resp);
        }

        private void LoadWebResp(WebResp r)
        {
            SourceAddress = r.ClientName;
            SourcePort = r.ClientPort;
            DestAddress = r.Request.Uri.ToString();
            DestPort = r.Request.Uri.Port;
            BytesSent = r.Request.ToString().Length;
            Protocol = r.Request.Uri.Scheme;
        }

        public string SourceAddress { get; private set; }
        public int SourcePort { get; private set; }
        public string DestAddress { get; private set; }
        public int DestPort { get; private set; }
        public long BytesSent { get; private set; }
        public string Protocol { get; private set; }

    }

}
