using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace telegen.Messages.Log
{
    public class Spawn : LogEvent
    {
        public Spawn(Process p, string userName, string commandLine = null) : base(p)
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

        public Spawn(string name, DateTime utcStart, int procId, string userName, string commandLine = null) : base(name, utcStart, procId)
        {
            UserName = userName;
            CommandLine = commandLine ?? string.Empty;
        }

        public string CommandLine { get; }
        public string UserName { get; }

        public override void CopyToDictionary(IDictionary<object, object> d)
        {
            d[nameof(UserName)] = UserName;
            d[nameof(CommandLine)] = CommandLine;
            base.CopyToDictionary(d);
        }
    }

}
