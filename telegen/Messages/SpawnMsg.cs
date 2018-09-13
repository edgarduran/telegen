using System.Collections.Generic;

namespace telegen.Messages
{
    public class SpawnMsg : MsgBase
    {
        public SpawnMsg(string exe, string arguments = null)
        {
            Executable = exe;
            Arguments = arguments ?? string.Empty;
        }
        public string Executable { get; }
        public string Arguments { get; }
    }

}
