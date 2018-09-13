using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.Event;
using telegen.Messages;
using telegen.Messages.Log;

namespace telegen.Actors
{
    public abstract class TelegenActor : ReceiveActor {
        protected ILoggingAdapter Log { get; }
        protected IActorRef ActivityLogger { get; }

        protected TelegenActor(IActorRef activityLogger = null) {
            Log = Context.GetLogger();
            ActivityLogger = activityLogger ?? ActorRefs.Nobody;
        }
    }

    public class SpawnActor : TelegenActor
    {
        public SpawnActor(IActorRef activityLogger = null) : base(activityLogger) {
            Become(Listening);
        }

        protected void Listening() {
            Receive<SpawnMsg>(m => Spawn(m));
        }

        void Spawn(SpawnMsg msg)
        {
            var p = System.Diagnostics.Process.Start(msg.Executable, msg.Arguments);
            var logEvt = new ProcessStartLog(p, Environment.UserName, msg.Arguments);
            ActivityLogger.Tell(logEvt, Self);

        }


    }
}
