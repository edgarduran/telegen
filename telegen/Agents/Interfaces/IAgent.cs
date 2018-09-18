using telegen.Operations;
using telegen.Results;

namespace telegen.Agents.Interfaces {
    public interface IAgent
    {
        Result Execute(Operation oper);
    }
}