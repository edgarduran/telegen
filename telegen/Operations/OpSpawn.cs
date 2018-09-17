using System.Collections.Generic;

namespace telegen.Operations
{
    public class OpSpawn : Operation
    {
        //public SpawnMsg(string exe, string arguments = null)
        //{
        //    Executable = exe;
        //    Arguments = arguments ?? string.Empty;
        //}

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
