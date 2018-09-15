using System;
using Akka.Actor;
using telegen.Agents;
using telegen.Messages;

namespace telegen.Actors
{
    public class NetworkActor : TelegenActor
    {
        protected Func<INetworkAgent> AgentBuilder { get; }

        public NetworkActor(IActorRef activityLogger = null, Func<INetworkAgent> agentBuilder = null) : base(activityLogger)
        {
            Receive<NetworkGetData>(m => MakeHttpCall(m), q => q.Protocol == "http" || q.Protocol == "https");
            Receive<WebResp>(m => WriteLog(m));
            AgentBuilder = agentBuilder ?? (() => new NetworkAgent());
        }

        private void MakeHttpCall(NetworkGetData msg)
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
