using System;
using System.Collections.Generic;
using System.Text;

namespace telegen.Operations
{
    /// <summary>
    /// Pause the engine for the given number of milliseconds.
    /// </summary>
    /// <seealso cref="telegen.Operations.Operation" />
    public class OpWait : Operation
    {
        public int Milliseconds { get; }

        public OpWait(int milliseconds) {
            Milliseconds = milliseconds;
        }
    }
}
