using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Akka.Actor;
using telegen.Agents;
using telegen.Operations;

namespace telegen.Actors
{
    public class ClientActor : TypedActor, IHandle<WebReq>
    {

        public ClientActor(INetworkAgent agent)
        {
            Agent = agent ?? new NetworkAgent();
        }

        protected INetworkAgent Agent { get; }

        public void Handle(WebReq req)
        {
            var results = Agent.Execute(req);
            Sender.Tell(results);
        }


    }

}
