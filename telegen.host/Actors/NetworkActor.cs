using System;
using Akka.Actor;
using Akka.Routing;
using telegen.Agents;
using telegen.Agents.Interfaces;
using telegen.Operations;
using telegen.Results;

namespace telegen.host.Actors
{
    public class NetworkActor : TelegenActor
    {
        private Func<IAgent> AgentBuilder { get; }
        private IActorRef client;

        public NetworkActor(IActorRef activityLogger = null, Func<IAgent> agentBuilder = null) : base(activityLogger)
        {
            AgentBuilder = agentBuilder ?? (() => new NetworkAgent());
            client = Context.ActorOf(Props.Create(() => new ClientActor(AgentBuilder())).WithRouter(new RoundRobinPool(5)));
            Receive<OpNetGet>(MakeHttpCall, q => q.Protocol == "http" || q.Protocol == "https");
            Receive<Result>(m => WriteLog(m));
         
        }

        private void MakeHttpCall(OpNetGet msg)
        {
            //var req = new WebReq(msg.Address, msg.Port);
            client.Tell(msg);
        }

        private void WriteLog(Result msg)
        {
            ActivityLogger.Tell(msg);
        }
    }

}
