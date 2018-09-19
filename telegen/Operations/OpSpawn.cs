using System.Collections.Generic;

namespace telegen.Operations
{
    /// <summary>
    /// Launch an application, optionally passing arguments via the command line.
    /// </summary>
    /// <seealso cref="telegen.Operations.Operation" />
    public class OpSpawn : Operation
    {
        public OpSpawn(string exe, IEnumerable<string> arguments = null)
        {
            Executable = exe;
            Arguments = string.Empty;
            if (arguments != null)
            {
                Arguments = string.Join(' ', arguments);
            }

        }

        public string Executable { get; }
        public string Arguments { get; private set; }
    }

}
