using System;
using telegen.Messages;
using telegen.Messages.Log;

namespace telegen.Agents
{
    public class ProcessAgent : IProcessAgent
    {
        public Spawn Spawn(SpawnMsg msg)
        {
            var p = System.Diagnostics.Process.Start(msg.Executable, msg.Arguments);
            return new Spawn(p, Environment.UserName, msg.Arguments);
        }
    }
}
