using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using Newtonsoft.Json;
using telegen.Operations;

namespace telegen.Results
{
    public static class ResultFactory
    {
        #region Static values.  Gather this information only once.
        private static string _thisProcessName = null;
        private static int _thisProcessId;
        private static string _thisProcessCommandLine;
        private static string _thisStartingFolder;
        private static string _thisUserName;
        private static DateTime _thisProcessStartTime;
        private static string _machine;
        private static readonly object ProcessLock = new object();
        #endregion

        private static void GetDefaults()
        {
            if (_thisProcessName == null)
            {
                lock (ProcessLock)
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
                        _machine = Environment.MachineName;
                    }
                }
            }
        }

        public static Result Create() => new Result();
        public static Result Create(Operation op) => new Result(op);

    }

    public class Result : DynamicBase
    {
        public Result()
        {

        }

        public Result(Operation op)
        {
            AsDynamic.domain = op.Domain;
            AsDynamic.action = op.Action;
        }

    }



}
