using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace telegen.Operations
{
    /// <summary>
    /// Requests that the framework modify a file. Currently, only
    /// append operations are supported.
    /// </summary>
    /// <seealso cref="telegen.Operations.FileOperation" />
    public class OpUpdateFile : FileOperation
    {
        public OpUpdateFile(string filename, string data) : base(filename)
        {
            Contents = data; 
        }

        public string Contents { get; }
    }

}
