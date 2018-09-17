using telegen.Operations;
using telegen.Operations.Results;

namespace telegen.Agents
{
    public class NullAgent : IAgent
    {
        public static IAgent Instance { get; } = new NullAgent();

        protected NullAgent()
        {
        }

        public Result Execute(Operation oper)
        {
            
            return (oper is OpNop)?
                  new NullResult(reason: $"Syntax error : {(oper as OpNop).Command}")
                : new NullResult($"Unknown operation code : {oper.GetType().Name}");
        }

    }
}