using System.Collections.Generic;
using System.Linq;

namespace telegen.Messages
{
    public class UpdateFileMsg : FileMessage
    {
        public UpdateFileMsg(string path, string filename, string data) : base(path, filename)
        {
            Contents = data.ToCharArray().Cast<byte>();
        }

        public IEnumerable<byte> Contents { get; }
    }

}
