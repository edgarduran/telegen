using System;
using Akka.Actor;
using telegen.Agents;
using telegen.Agents.Interfaces;
using telegen.Operations;

namespace telegen.host.Actors
{
    public class ClientActor : ReceiveActor
    {
        protected IAgent Agent { get; }

        public ClientActor(IAgent agent) {
            Agent = agent;
            Receive<OpNetGet>(nm => Handle(nm));
        }

        protected void Handle(OpNetGet netGet) {
            var results = Agent.Execute(netGet);
            if (results != null) Sender.Tell(results);
        }


    }

}
