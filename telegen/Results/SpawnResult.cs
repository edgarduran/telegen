using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace telegen.Results
{
    public class SpawnResult : Result
    {
        public SpawnResult(Process p, string userName, string parameters = null)
        {
            try
            {
                SpawnedProcessId = p.Id;
                SpawnedProcess = p.ProcessName;
                SpawnedProcessUserId = string.IsNullOrWhiteSpace(p.StartInfo.UserName) ? userName : p.StartInfo.UserName;
                SpawnedProcessParameters = p.StartInfo.Arguments;
            }
            catch (InvalidOperationException)
            {
                SpawnedProcessUserId = userName;
                SpawnedProcessParameters = parameters ?? string.Empty;
            }
        }

        //[Obsolete("Use only for unit-testing.")]
        //public SpawnResults(string name, DateTime utcStart, int procId, string userName, string parameters = null) : base(name, utcStart, procId)
        //{
        //    SpawnedProcessUserId = userName;
        //    SpawnedProcessParameters = parameters ?? string.Empty;
        //}

        public string SpawnedProcessUserId { get; }
        public string SpawnedProcess { get; }
        public int SpawnedProcessId { get; }
        public string SpawnedProcessParameters { get; }

        public override void CopyToDictionary(IDictionary<object, object> d)
        {
            d[nameof(SpawnedProcessUserId)] = SpawnedProcessUserId;
            d[nameof(SpawnedProcess)] = SpawnedProcess;
            d[nameof(SpawnedProcessId)] = SpawnedProcessId;
            d[nameof(SpawnedProcessParameters)] = SpawnedProcessParameters;
            base.CopyToDictionary(d);
        }
    }

}
