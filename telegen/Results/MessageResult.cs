using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace telegen.Results {
    public class MessageResult : Result
    {
        public string Message { get; }

        public MessageResult(string message) {
            Message = message;
        }

        public MessageResult(string message, Process p) : base(p)
        {
            Message = message;
        }

        public MessageResult(string message, string processName, DateTime utcStart, int procId) : base(processName, utcStart, procId)
        {
            Message = message;
        }

        public override void CopyToDictionary(IDictionary<object, object> d) {
            base.CopyToDictionary(d);
            d[nameof(Message)] = Message;
        }
    }
}