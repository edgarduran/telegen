using telegen.Operations;
using telegen.Operations.Results;

namespace telegen.Agents {
    public interface IAgent
    {
        Result Execute(Operation oper);
    }
}