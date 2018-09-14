using System;

namespace telegen.Messages.Log
{
    public abstract class LogEvent : MsgBase
    {
        protected readonly string _thisProcessName;
        protected readonly int _thisProcessId;
        protected readonly string _thisProcessCommandLine;
        protected readonly DateTime _thisProcessStartTime;
        protected static object _processLock = new object();

        protected LogEvent()
        {
            if (_thisProcessName == null)
            {
                lock (_processLock)
                {
                    if (_thisProcessName == null)
                    {
                        var p = System.Diagnostics.Process.GetCurrentProcess();
                        _thisProcessName = p.ProcessName;
                        _thisProcessId = p.Id;
                        _thisProcessStartTime = p.StartTime.ToUniversalTime();
                        _thisProcessCommandLine = Environment.CommandLine;
                    }
                }
            }
            ProcessName = _thisProcessName;
            UTCStart = _thisProcessStartTime;
            ProcessId = _thisProcessId;

        }


        protected LogEvent(string processName, DateTime utcStart, int procId)
        {
            ProcessName = processName;
            UTCStart = utcStart;
            ProcessId = procId;
        }

        protected LogEvent(System.Diagnostics.Process p) : this (p.ProcessName, p.StartTime.ToUniversalTime(), p.Id) {
        }

        public string ProcessName { get; }
        public DateTime UTCStart { get; protected set; }
        public string TimeString => $"{UTCStart:yyyyMMddhhmmsstttt}"; //TODO: Fix this format string!
        public int ProcessId { get; }

    }


}
