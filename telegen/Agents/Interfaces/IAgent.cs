using telegen.Operations;
using telegen.Results;

namespace telegen.Agents.Interfaces {

    /// <summary>
    /// Generic agent interface used by the scripting engine.
    /// </summary>
    public interface IAgent
    {
        /// <summary>
        /// Executes the specified operation and returns the result.
        /// </summary>
        /// <param name="oper">The operation... a description of the action that needs to happen.</param>
        /// <returns>The results of the operation, or null if no action was taken.</returns>
        Result Execute(Operation oper);
    }
}