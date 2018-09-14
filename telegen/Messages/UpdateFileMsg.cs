using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace telegen.Messages
{
    public class UpdateFileMsg : FileMessage
    {
        public UpdateFileMsg(string path, string filename, string data) : base(path, filename)
        {
            Contents = Encoding.ASCII.GetBytes(data); // Actual contents doesn't really matter, so long as we have data.
        }

        public IEnumerable<byte> Contents { get; }
    }

}
