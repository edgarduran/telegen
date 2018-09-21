using System;
using System.Diagnostics;

namespace telegen.Results
{

    /// <summary>
    /// Retrieves the information about the telegen process one time, and 
    /// stores it in a singleton for inclusion in result messages.
    /// </summary>
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



}
