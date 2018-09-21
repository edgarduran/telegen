using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using Newtonsoft.Json;
using telegen.Messages;

namespace telegen.Results
{
    public static class ProcessInfoFactory
    {
        #region Static values.  Gather this information only once.
        private static string ProcessName = null;
        private static int ProcessId;
        private static string CommandLine;
        private static string StartingFolder;
        private static string UserName;
        private static DateTime TimeStamp;
        private static string Machine;
        private static readonly object ProcessLock = new object();
        #endregion

        static ProcessInfoFactory()
        {

        }
    }

    public class ProcessInfo
    {
        private static readonly object InitializationLock = new object();

        static ProcessInfo()
        {
            // GRAB THE TIMESTAMP HERE!!!
            if (Instance == null)
            {
                lock (InitializationLock)
                {
                    if (Instance == null)
                    {
                        var pi = new ProcessInfo();
                        var p = Process.GetCurrentProcess();
                        pi.ProcessId = p.Id;
                        pi.ProcessName = p.ProcessName;
                        pi.CommandLine = Environment.CommandLine;
                        pi.Machine = Environment.MachineName;
                        pi.UserName = Environment.UserName;
                        pi.CurrentFolder = Environment.CurrentDirectory;

                        Instance = pi;
                    }
                }
            }
        }

        protected ProcessInfo()
        {
        }

        public static ProcessInfo Instance { get; }

        public string ProcessName { get; protected set; }
        public int ProcessId { get; protected set; }
        public string CommandLine { get; protected set; }
        public string CurrentFolder { get; protected set; }
        public string UserName { get; protected set; }
        public string Machine { get; protected set; }

        public void Stamp(dynamic msg)
        {
            msg.processId = ProcessId;
            msg.processName = ProcessName;
            msg.commandLine = CommandLine;
            msg.machine = Machine;
            msg.userName = UserName;
            msg.currentFolder = CurrentFolder;
        }
    }

    public class Result : MessageBase
    {
        public Result()
        {
            AsDynamic.timeStampUtc = DateTime.UtcNow;
        }

        public Result(Operation op)
        {
            Clear("timeStampUtc");
            Domain = op.Domain;
            Action = op.Action;
            this.AsDynamic.timeStampUtc = DateTime.UtcNow;
            ProcessInfo.Instance.Stamp(this.AsDynamic);
        }

        [JsonProperty("domain")]
        public string Domain
        {
            get
            {
                GetPropertyByName(nameof(Domain), out object value);
                return (string)value;
            }
            set
            {
                SetPropertyByName(nameof(Domain), value);
            }
        }

        [JsonProperty("action")]
        public string Action
        {
            get
            {
                GetPropertyByName(nameof(Action), out object value);
                return (string)value;
            }
            set
            {
                SetPropertyByName(nameof(Action), value);
            }
        }

    }



}
