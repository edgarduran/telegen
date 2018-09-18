using System;
using telegen.Agents.Interfaces;
using telegen.Operations;
using telegen.Results;

namespace telegen.Agents
{
    public class ProcessAgent : IProcessAgent, IAgent
    {
        public SpawnResults Spawn(OpSpawn msg)
        {
            var p = System.Diagnostics.Process.Start(msg.Executable, msg.Arguments);
            return new SpawnResults(p, Environment.UserName, msg.Arguments);
        }

        public Result Execute(Operation oper)
        {
            return oper is OpSpawn ?
                Spawn(oper as OpSpawn) as Result :
                new NullResult($"{GetType().Name} was invoked with an unsupported operation type ({oper.GetType().Name}).");
        }
    }
}
