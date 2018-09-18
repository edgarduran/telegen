using telegen.Agents.Interfaces;
using telegen.Operations;
using telegen.Results;

namespace telegen.Agents {
    public class WaitAgent : IAgent {
        public Result Execute(Operation oper) {
            return oper is OpWait ?
                Wait((oper as OpWait).Milliseconds) as Result :
                new NullResult($"{GetType().Name} was invoked with an unsupported operation type ({oper.GetType().Name}).");
        }

        protected Result Wait(int ms) {
            System.Threading.Thread.Sleep(ms);
            return new MessageResult($"Script was paused for {ms} milliseconds...");
        }

    }
}