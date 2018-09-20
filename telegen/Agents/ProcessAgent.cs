using System;
using telegen.Agents.Interfaces;
using telegen.Operations;
using telegen.Results;

namespace telegen.Agents
{
    public class ProcessAgent : Agent
    {
        public override Result Execute(Operation oper)
        {
            Guard(oper, "Spawn");
            return Spawn(oper);
        }

        protected SpawnResult Spawn(Operation msg)
        {
            Guard(msg, "Spawn");
            var (executable, arguments) = msg.Require<string, string>("executable", "arguments");
            var p = System.Diagnostics.Process.Start(executable, arguments);
            return new SpawnResult(p, Environment.UserName, arguments);
        }

    }
}
