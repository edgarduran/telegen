using System;
using System.Diagnostics;

namespace telegen.Messages.Log
{
    public abstract class LogEvent : MsgBase
    {
        protected LogEvent(string name, DateTime utcStart, int procId)
        {
            ProcessName = name;
            UTCStart = utcStart;
            ProcessId = procId;
        }

        protected LogEvent(System.Diagnostics.Process p) : this (p.ProcessName, p.StartTime.ToUniversalTime(), p.Id) {
        }

        public string ProcessName { get; }
        public DateTime UTCStart { get; }
        public string TimeString => $"{UTCStart:yyyyMMddhhmmsstttt}"; //TODO: Fix this format string!
        public int ProcessId { get; }

        //public static LogEvent Current()
        //{
        //    var p = System.Diagnostics.Process.GetCurrentProcess();
        //    var pi = new LogEvent(p);
        //    return pi;
        //}
    }

    public class ProcessStartLog : LogEvent {
        public ProcessStartLog(Process p, string userName, string commandLine = null) : base(p)
        {
            try {
                UserName = string.IsNullOrWhiteSpace(p.StartInfo.UserName) ? userName : p.StartInfo.UserName;
                CommandLine = p.StartInfo.Arguments;
            } catch (InvalidOperationException ioe) {
                UserName = userName;
                CommandLine = commandLine ?? string.Empty;
            }
        }

        public ProcessStartLog(string name, DateTime utcStart, int procId, string userName, string commandLine = null) : base(name, utcStart, procId) {
            UserName = userName;
            CommandLine = commandLine ?? string.Empty;
        }

        public string CommandLine { get; }
        public string UserName { get; }
    }


}
