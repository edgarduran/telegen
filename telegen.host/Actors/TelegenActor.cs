using Akka.Actor;
using Akka.Event;

namespace telegen.host.Actors
{
    public abstract class TelegenActor : ReceiveActor
    {
        protected ILoggingAdapter Log { get; }
        protected IActorRef ActivityLogger { get; }

        protected TelegenActor(IActorRef activityLogger = null)
        {
            Log = Context.GetLogger();
            ActivityLogger = activityLogger ?? ActorRefs.Nobody;
        }
    }
}
