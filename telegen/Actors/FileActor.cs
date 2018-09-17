using System;
using System.IO;
using System.Linq;
using Akka.Actor;
using telegen.Agents;
using telegen.Operations;
using telegen.Operations.Results;

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
            Receive<OpCreateFile>(m => CreateFile(m));
            Receive<OpUpdateFile>(m => UpdateFile(m));
            Receive<OpDeleteFile>(m => DeleteFile(m));
        }

        protected void CreateFile(OpCreateFile msg)
        {
            var results = Agent.CreateFile(msg);
            ActivityLogger.Tell(results, Self);
        }

        protected void UpdateFile(OpUpdateFile msg)
        {
            var results = Agent.UpdateFile(msg);
            if (results != null)
            {
                ActivityLogger.Tell(results, Self);
            }
        }

        protected void DeleteFile(OpDeleteFile msg)
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
