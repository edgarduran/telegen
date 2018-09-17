using System;
using Akka.Actor;
using telegen.Agents;
using telegen.Operations;

namespace telegen.Actors
{
    public class NetworkActor : TelegenActor
    {
        protected Func<INetworkAgent> AgentBuilder { get; }

        public NetworkActor(IActorRef activityLogger = null, Func<INetworkAgent> agentBuilder = null) : base(activityLogger)
        {
            Receive<OpNetGet>(m => MakeHttpCall(m), q => q.Protocol == "http" || q.Protocol == "https");
            Receive<WebResp>(m => WriteLog(m));
            AgentBuilder = agentBuilder ?? (() => new NetworkAgent());
        }

        private void MakeHttpCall(OpNetGet msg)
        {
            var req = new WebReq(msg.Address, msg.Port);
            var client = Context.ActorOf(Props.Create(() => new ClientActor(AgentBuilder())));
            client.Tell(req);
        }

        private void WriteLog(WebResp msg)
        {
            ActivityLogger.Tell(msg);
        }
    }

}
