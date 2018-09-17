using System;
using System.Diagnostics;

namespace telegen.Operations.Results
{
    public class NullResult : Result
    {
        public NullResult(string reason)
        {
            Reason = reason;
        }

        public NullResult(string reason, Process p) : base(p)
        {
            Reason = reason;
        }

        public NullResult(string reason, string processName, DateTime utcStart, int procId) : base(processName, utcStart, procId)
        {
            Reason = reason;
        }

        public string Reason { get; }
    }
}
