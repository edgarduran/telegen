using System.Collections.Generic;
using telegen.Operations;
using telegen.Results;

namespace telegen
{
    /// <summary>
    /// Describes the interface for the script engine.
    /// </summary>
    public interface IScriptEngine
    {
        IEnumerable<Result> ParseAndExecute(string jsonScript);

        /// <summary>
        /// Executes the script contained in the specified filename.
        /// </summary>
        /// <param name="filename">The script filename.</param>
        /// <remarks>
        /// Most implementations of this command will read the contents of the file into
        /// a string array, and then forward them to the <see cref="Execute(IEnumerable&lt;string&gt;)"/> method.
        /// </remarks>
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