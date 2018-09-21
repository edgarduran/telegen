using telegen.Agents.Interfaces;
using telegen.Messages;
using telegen.Results;

namespace telegen.Agents {
    public class SystemAgent : Agent {

        public override Result Execute(Operation oper) {
            Guard(oper, "Wait", "Comment");
            if (oper.Action == "Comment") return null;
            return Wait(oper);
        }

        protected Result Wait(Operation msg) {
            var ms =(int) msg.Require<long>("ms");
            System.Threading.Thread.Sleep(ms);
            return null; // output not requested for this task. Don't confuse the output.
        }

    }
}