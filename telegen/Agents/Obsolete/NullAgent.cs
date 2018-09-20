using telegen.Agents.Interfaces;
using telegen.Operations;
using telegen.Results;

namespace telegen.Agents
{
    /// <summary>
    /// An agent that receives any operation, performs no task, and returns a <see cref="NullResult"/>. This
    /// is sometimes created by the engine when the appropriate agent cannot be found for an operation. If so,
    /// the result will attempt to indicate why this happened.
    /// </summary>
    /// <seealso cref="telegen.Agents.Interfaces.IAgent" />
    public class NullAgent : IAgent
    {
        public static IAgent Instance { get; } = new NullAgent();

        protected NullAgent()
        {
        }

        public Result Execute(Operation oper)
        {
            
            return (oper is OpNop nop)?
                  new NullResult($"Syntax error : {nop.Command}")
                : new NullResult($"Unknown operation code : {oper.GetType().Name}");
        }

    }
}