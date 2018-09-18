using Akka.Actor;

namespace telegen
{
    public static class RootActors
    {
        public static IActorRef CommandParser { get; private set; }
        public static IActorRef Log { get; private set; }
    }
}
