using System;
using Akka.Actor;

namespace telegen
{
    public class ActorSystemHost : IDisposable
    {
        ActorSystem _system = null;
        public ActorSystemHost()
        {
            _system = ActorSystem.Create("TeleGen");

        }

        public void Dispose()
        {
            _system.Dispose();
        }
    }
}
