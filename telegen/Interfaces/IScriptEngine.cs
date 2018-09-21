using System.Collections.Generic;
using telegen.Messages;
using telegen.Results;

namespace telegen.Interfaces
{
    /// <summary>
    /// Translates the script into operations, and dispatches each
    /// requested operation to the appropriate agent. Returns the
    /// Result message returned by the agent.
    /// </summary>
    public interface IScriptEngine
    {
        /// <summary>
        /// Parses and executes the <paramref name="jsonScript"/>.
        /// </summary>
        /// <remarks>
        /// Most implementations of this command will split the contents of the script into
        /// a string array, and then forward them to the <see cref="Execute(IEnumerable&lt;string&gt;)"/> method.
        /// </remarks>
        /// <returns>An enumeration of the results of each operation.</returns>
        /// <param name="jsonScript">The script in json format.</param>
        IEnumerable<Result> ParseAndExecute(string jsonScript);

        /// <summary>
        /// Executes the script contained in the specified filename.
        /// </summary>
        /// <remarks>
        /// Most implementations of this command will read the script from the file and forward
        /// it to the <see cref="ParseAndExecute(string)"/> method.
        /// </remarks>
        /// <param name="filename">The script filename.</param>

        /// <returns>A list of <see cref="telegen.Results"/> returned by the script.</returns>
        IEnumerable<Result> Execute(string filename);


        /// <summary>
        /// Executes the specified script.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <returns>A list of <see cref="telegen.Results"/> returned by the script.</returns>
        IEnumerable<Result> Execute(IEnumerable<Operation> script);
    }
}