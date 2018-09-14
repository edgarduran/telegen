using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Akka.Actor;
using telegen.Messages;
using telegen.Messages.Log;

namespace telegen.Actors
{

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

    public class FileActor : TelegenActor
    {
        public FileActor(IActorRef activityLogger = null) : base(activityLogger)
        {
            Become(Listening);
        }

        protected void Listening()
        {
            Receive<CreateFileMsg>(m => CreateFile(m));
            Receive<UpdateFileMsg>(m => UpdateFile(m));
            Receive<DeleteFileMsg>(m => DeleteFile(m));
        }

        protected void CreateFile(CreateFileMsg msg)
        {
            File.WriteAllText(msg.FullName, string.Empty);
            var fi = new FileInfo(msg.FullName);
            var results = new ProcessFileActivityLog(fi.CreationTimeUtc, msg.FullName, FileEventType.Create, Environment.UserName);
            ActivityLogger.Tell(results, Self);
        }

        protected void UpdateFile(UpdateFileMsg msg)
        {
            if (File.Exists(msg.FullName))
            {
                File.WriteAllBytes(msg.FullName, msg.Contents.ToArray());
                var fi = new FileInfo(msg.FullName);
                var results = new ProcessFileActivityLog(fi.LastWriteTimeUtc, msg.FullName, FileEventType.Update, Environment.UserName);
                ActivityLogger.Tell(results, Self);
            }
        }

        protected void DeleteFile(DeleteFileMsg msg)
        {
            //TODO: What do I do if the requested event is not performed?
            if (File.Exists(msg.FullName))
            {
                File.Delete(msg.FullName);
                var results = new ProcessFileActivityLog(DateTime.UtcNow, msg.FullName, FileEventType.Delete, Environment.UserName);
                ActivityLogger.Tell(results, Self);
            }
        }

    }
}
