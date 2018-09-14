using Akka.Actor;
using telegen.Messages;

namespace telegen.Actors
{
    public class NetworkActor : TelegenActor
    {
        public NetworkActor(IActorRef activityLogger = null) : base(activityLogger)
        {
            Receive<NetworkGetData>(m => MakeHttpCall(m), q => q.Protocol == "http" || q.Protocol == "https");
            Receive<WebResp>(m => WriteLog(m));
        }

        private void MakeHttpCall(NetworkGetData msg)
        {
            var req = new WebReq(msg.Address, msg.Port);
            var client = Context.ActorOf<ClientActor>();
            client.Tell(req);
        }

        private void WriteLog(WebResp msg)
        {
            ActivityLogger.Tell(msg);
        }
    }

}
