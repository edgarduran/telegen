using telegen.Operations;
using telegen.Operations.Results;

namespace telegen.Agents
{
    public interface IFileAgent
    {
        FileActivityResult CreateFile(OpCreateFile msg);
        FileActivityResult DeleteFile(OpDeleteFile msg);
        FileActivityResult UpdateFile(OpUpdateFile msg);
    }

    public interface IAgent
    {
        Result Execute(Operation oper);
    }


    public class NullAgent : IAgent
    {
        public static IAgent Instance { get; } = new NullAgent();

        protected NullAgent()
        {
        }

        public Result Execute(Operation oper)
        {
            return new NullResult($"Unknown operation code : {oper.GetType().Name}");
        }

    }
}