using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace telegen.Operations.Results
{
    public abstract class Result 
    {
        protected readonly string _thisProcessName;
        protected readonly int _thisProcessId;
        protected readonly string _thisProcessCommandLine;
        protected readonly string _thisUserName;
        protected readonly DateTime _thisProcessStartTime;
        protected static object _processLock = new object();

        protected Result()
        {
            if (_thisProcessName == null)
            {
                lock (_processLock)
                {
                    if (_thisProcessName == null)
                    {
                        var p = Process.GetCurrentProcess();
                        _thisProcessName = p.ProcessName;
                        _thisProcessId = p.Id;
                        _thisProcessStartTime = p.StartTime.ToUniversalTime();
                        _thisUserName = Environment.UserName;
                        _thisProcessCommandLine = Environment.CommandLine;
                    }
                }
            }
            UserName = _thisUserName;
            ProcessName = _thisProcessName;
            UTCStart = _thisProcessStartTime;
            ProcessId = _thisProcessId;

        }


        protected Result(string processName, DateTime utcStart, int procId)
        {
            ProcessName = processName;
            UTCStart = utcStart;
            ProcessId = procId;
        }

        protected Result(Process p) : this (p.ProcessName, p.StartTime.ToUniversalTime(), p.Id) {
        }

        public string ResultType => GetType().Name;
        public string ProcessName { get; }
        public string TimeString => $"{UTCStart:u}";
        public int ProcessId { get; }
        public DateTime UTCStart { get; protected set; }
        public string UserName { get; protected set; }
        public string CommandLine { get; protected set; }

        public virtual void CopyToDictionary(IDictionary<object, object> d)
        {
            d["_Type"] = GetType().Name.Replace("Result", string.Empty);
            d[nameof(ProcessName)] = ProcessName;
            d[nameof(UTCStart)] = TimeString;
            d[nameof(ProcessId)] = ProcessId.ToString();
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }

}
