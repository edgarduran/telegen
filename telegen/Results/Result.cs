using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace telegen.Results
{
    //todo: Add appropriate JsonProperty attributes to these classes
    public abstract class Result {
        protected static string _thisProcessName = null;
        protected static int _thisProcessId;
        protected static string _thisProcessCommandLine;
        protected static string _thisStartingFolder;
        protected static string _thisUserName;
        protected static DateTime _thisProcessStartTime;
        protected static string _Machine;
        protected static object _processLock = new object();


        protected static void GetDefaults() {
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
                        _thisStartingFolder = Environment.CurrentDirectory;
                        _Machine = Environment.MachineName;
                    }
                }
            }
        }

        protected Result() {
            GetDefaults();
            ProcessName = _thisProcessName;
            ProcessId = _thisProcessId;
            UTCStart = _thisProcessStartTime;                        
            UserName = _thisUserName;
            CommandLine = _thisProcessCommandLine;
            StartingFolder = _thisStartingFolder;
            Machine = _Machine;
        }


        protected Result(string processName, DateTime utcStart, int procId): this() {
            ProcessName = processName;
            UTCStart = utcStart;
            ProcessId = procId;            
        }

        protected Result(Process p) : this (p.ProcessName, p.StartTime.ToUniversalTime(), p.Id)  {            
        }

        public string ResultType => GetType().Name;
        public string ProcessName { get; }
        public string TimeString => $"{UTCStart:u}";
        public int ProcessId { get; }
        public DateTime UTCStart { get; protected set; }
        public string UserName { get; protected set; }
        public string CommandLine { get; protected set; }
        public string StartingFolder { get; protected set; }
        public string Machine { get; protected set; }

        public virtual void CopyToDictionary(IDictionary<object, object> d)
        {
            d["Type"] = GetType().Name.Replace("Result", string.Empty);
            d[nameof(ResultType)] = ResultType;
            d[nameof(ProcessName)] = ProcessName;
            d[nameof(UTCStart)] = TimeString;
            d[nameof(ProcessId)] = ProcessId.ToString();
            d[nameof(UserName)] = UserName;
            d[nameof(CommandLine)] = CommandLine;
            d[nameof(StartingFolder)] = StartingFolder;
            d[nameof(Machine)] = Machine;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }

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
