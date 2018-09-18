using Akka.Actor;
using telegen.Agents;
using telegen.Operations;

namespace telegen.host.Actors
{

    public class ProcessActor : TelegenActor
    {
        public ProcessActor(IActorRef activityLogger = null, IProcessAgent agent = null) : base(activityLogger) {
            Become(Listening);
            Agent = agent ?? new ProcessAgent();
        }

        protected IProcessAgent Agent { get; }

        protected void Listening() {
            Receive<OpSpawn>(m => Spawn(m));
        }

        void Spawn(OpSpawn msg)
        {
            var results = Agent.Spawn(msg);
            ActivityLogger.Tell(results, Self);
        }

    }
}
