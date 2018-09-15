using System;
using System.IO;
using System.Linq;
using telegen.Messages;
using telegen.Messages.Log;

namespace telegen.Agents
{
    public class FileAgent : IFileAgent
    {
        public ProcessFileActivityLog CreateFile(CreateFileMsg msg)
        {
            File.WriteAllText(msg.FullName, string.Empty);
            var fi = new FileInfo(msg.FullName);
            return new ProcessFileActivityLog(fi.CreationTimeUtc, msg.FullName, FileEventType.Create, Environment.UserName);
        }

        public ProcessFileActivityLog UpdateFile(UpdateFileMsg msg)
        {
            if (File.Exists(msg.FullName))
            {
                File.WriteAllBytes(msg.FullName, msg.Contents.ToArray());
                var fi = new FileInfo(msg.FullName);
                return new ProcessFileActivityLog(fi.LastWriteTimeUtc, msg.FullName, FileEventType.Update, Environment.UserName);
            }
            return null;
        }

        public ProcessFileActivityLog DeleteFile(DeleteFileMsg msg)
        {
            //TODO: What do I do if the requested event is not performed?
            if (File.Exists(msg.FullName))
            {
                File.Delete(msg.FullName);
                return new ProcessFileActivityLog(DateTime.UtcNow, msg.FullName, FileEventType.Delete, Environment.UserName);
                //ActivityLogger.Tell(results, Self);
            }
            return null;
        }
    }
}
