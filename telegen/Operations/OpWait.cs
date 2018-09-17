using System;
using System.Collections.Generic;
using System.Text;

namespace telegen.Operations
{
    public class OpWait : Operation
    {
        public int Milliseconds { get; }

        public OpWait(int milliseconds) {
            Milliseconds = milliseconds;
        }
    }
}
