using telegen.Agents.Interfaces;
using telegen.Operations;
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
            throw new System.Exception("Fix this.");
//            return new MessageResult($"Script was paused for {ms} milliseconds...");
        }

    }
}