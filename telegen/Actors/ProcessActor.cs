using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using telegen.Agents;
using telegen.Messages;
using telegen.Messages.Log;

namespace telegen.Actors
{

    public class ProcessActor : TelegenActor
    {
        public ProcessActor(IActorRef activityLogger = null, IProcessAgent agent = null) : base(activityLogger) {
            Become(Listening);
            Agent = agent ?? new ProcessAgent();
        }

        protected IProcessAgent Agent { get; }

        protected void Listening() {
            Receive<SpawnMsg>(m => Spawn(m));
        }

        void Spawn(SpawnMsg msg)
        {
            var results = Agent.Spawn(msg);
            ActivityLogger.Tell(results, Self);
        }

    }
}
