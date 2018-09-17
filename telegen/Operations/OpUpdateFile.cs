using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace telegen.Operations
{
    public class OpUpdateFile : FileOperation
    {
        public OpUpdateFile(string filename, string data) : base(filename)
        {
            Contents = data; 
        }

        public string Contents { get; }
    }

}
