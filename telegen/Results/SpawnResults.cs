using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace telegen.Results
{
    public class SpawnResults : Result
    {
        public SpawnResults(Process p, string userName, string parameters = null) : base(p)
        {
            try
            {
                UserName = string.IsNullOrWhiteSpace(p.StartInfo.UserName) ? userName : p.StartInfo.UserName;
                Parameters = p.StartInfo.Arguments;
            }
            catch (InvalidOperationException)
            {
                UserName = userName;
                Parameters = parameters ?? string.Empty;
            }
        }

        public SpawnResults(string name, DateTime utcStart, int procId, string userName, string parameters = null) : base(name, utcStart, procId)
        {
            UserName = userName;
            Parameters = parameters ?? string.Empty;
        }

        public string Parameters { get; }

        public override void CopyToDictionary(IDictionary<object, object> d)
        {
            d[nameof(UserName)] = UserName;
            d[nameof(Parameters)] = Parameters;
            base.CopyToDictionary(d);
        }
    }

}
