using System;
using telegen.Agents.Interfaces;
using telegen.Messages;
using telegen.Results;

namespace telegen.Agents
{
    /// <summary>
    /// Exercises the Process domain.
    /// <para>
    /// Domain-specific fields:
    /// </para>
    /// <para>
    /// <list type="bullet">
    ///     <item>Process name</item>
    ///     <item>Process command line</item>
    ///     <item>Process id</item>
    /// </list>
    /// </para>
    /// </summary>
    public class ProcessAgent : Agent
    {
        public override Result Execute(Operation oper)
        {
            Guard(oper, "Spawn");
            return Spawn(oper);
        }

        protected Result Spawn(Operation msg)
        {
            Guard(msg, "Spawn");
            var (executable, arguments) = msg.Require<string, string>("executable", "arguments");
            var p = System.Diagnostics.Process.Start(executable, arguments);

            dynamic r = new Result(msg);
            r.appName = p.ProcessName;
            r.appCommandLine = p.StartInfo.Arguments;
            r.appProcessId = p.Id;

            return r;
        }

    }
}
