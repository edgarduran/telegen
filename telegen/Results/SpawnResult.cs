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

        public string SpawnedProcessUserId { get; }
        public string SpawnedProcess { get; }
        public int SpawnedProcessId { get; }
        public string SpawnedProcessParameters { get; }

    }

}
