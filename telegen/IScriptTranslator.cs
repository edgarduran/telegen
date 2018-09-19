using System.Collections.Generic;
using telegen.Operations;

namespace telegen
{
    /// <summary>
    /// Script translators are responsible for parsing the script and returning
    /// the appropriate <see cref="Operation"/> objects. This architecture permits
    /// us to develop multiple script languages, all of which speak the common language
    /// of "<c>IEnumerable&lt;Operation&gt;</c>". This is similar to the way that multiple
    /// languages compile to Java bytecode or to .Net MSIL.
    /// </summary>
    public interface IScriptTranslator
    {

        /// <summary>
        /// Translates the script contained in the specified filename.
        /// </summary>
        /// <param name="filename">The script filename.</param>
        /// <returns>A list of <see cref="Operation"/> objects represented by the script.</returns>
        /// <remarks>
        /// Most implementations of this command will read the contents of the file into
        /// a string array, and then forward them to the <see cref="Translate(IEnumerable&lt;string&gt;)"/> method.
        /// </remarks>
        IEnumerable<Operation> Translate(string filename);


        /// <summary>
        /// Translates the specified script.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <returns>A list of <see cref="Operation"/> objects represented by the script.</returns>
        IEnumerable<Operation> Translate(IEnumerable<string> script);
    }
}