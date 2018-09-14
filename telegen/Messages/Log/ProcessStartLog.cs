using System;
using System.Diagnostics;

namespace telegen.Messages.Log
{
    public class ProcessStartLog : LogEvent
    {
        public ProcessStartLog(Process p, string userName, string commandLine = null) : base(p)
        {
            try
            {
                UserName = string.IsNullOrWhiteSpace(p.StartInfo.UserName) ? userName : p.StartInfo.UserName;
                CommandLine = p.StartInfo.Arguments;
            }
            catch (InvalidOperationException)
            {
                UserName = userName;
                CommandLine = commandLine ?? string.Empty;
            }
        }

        public ProcessStartLog(string name, DateTime utcStart, int procId, string userName, string commandLine = null) : base(name, utcStart, procId)
        {
            UserName = userName;
            CommandLine = commandLine ?? string.Empty;
        }

        public string CommandLine { get; }
        public string UserName { get; }
    }

}
