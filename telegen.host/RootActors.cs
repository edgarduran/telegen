using Akka.Actor;

namespace telegen.host
{
    public static class RootActors
    {
        public static IActorRef CommandParser { get; private set; }
        public static IActorRef Log { get; private set; }
    }
}
