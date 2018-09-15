using System;
using System.IO;
using System.Linq;
using Akka.Actor;
using telegen.Agents;
using telegen.Messages;
using telegen.Messages.Log;

namespace telegen.Actors
{
    public class FileActor : TelegenActor
    {
        protected IFileAgent Agent { get; }

        public FileActor(IActorRef activityLogger = null, IFileAgent agent = null) : base(activityLogger)
        {
            Become(Listening);
            Agent = agent ?? new FileAgent(); // Provide a default implementation
        }

        protected void Listening()
        {
            Receive<CreateFileMsg>(m => CreateFile(m));
            Receive<UpdateFileMsg>(m => UpdateFile(m));
            Receive<DeleteFileMsg>(m => DeleteFile(m));
        }

        protected void CreateFile(CreateFileMsg msg)
        {
            var results = Agent.CreateFile(msg);
            ActivityLogger.Tell(results, Self);
        }

        protected void UpdateFile(UpdateFileMsg msg)
        {
            var results = Agent.UpdateFile(msg);
            if (results != null)
            {
                ActivityLogger.Tell(results, Self);
            }
        }

        protected void DeleteFile(DeleteFileMsg msg)
        {
            //TODO: What do I do if the requested event is not performed?
            var results = Agent.DeleteFile(msg);
            if (results != null)
            {
                ActivityLogger.Tell(results, Self);
            }
        }

    }
}
