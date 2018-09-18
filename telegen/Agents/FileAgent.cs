using System;
using System.IO;
using System.Linq;
using telegen.Agents.Interfaces;
using telegen.Operations;
using telegen.Results;

namespace telegen.Agents
{
    public class FileAgent : IFileAgent, IAgent
    {
        public FileActivityResult CreateFile(OpCreateFile msg)
        {
            File.WriteAllText(msg.FullName, string.Empty);
            var fi = new FileInfo(msg.FullName);
            return new FileActivityResult(fi.CreationTimeUtc, msg.FullName, FileEventType.Create, Environment.UserName);
        }

        public FileActivityResult UpdateFile(OpUpdateFile msg)
        {
            if (File.Exists(msg.FullName))
            {
                File.AppendAllText(msg.FullName, msg.Contents);
                var fi = new FileInfo(msg.FullName);                
                return new FileActivityResult(fi.LastWriteTimeUtc, msg.FullName, FileEventType.Update, Environment.UserName);
            }
            return null;
        }

        public FileActivityResult DeleteFile(OpDeleteFile msg)
        {
            //TODO: What do I do if the requested event is not performed?
            if (File.Exists(msg.FullName))
            {
                File.Delete(msg.FullName);
                return new FileActivityResult(DateTime.UtcNow, msg.FullName, FileEventType.Delete, Environment.UserName);

            }
            return null;
        }

        public Result Execute(Operation oper)
        {
            Result result = null;
            switch (oper.GetType().Name)
            {
                case nameof(OpCreateFile): 
                    {
                        result = CreateFile(oper as OpCreateFile);
                        break;
                    }
                case nameof(OpUpdateFile):
                    {
                        result = UpdateFile(oper as OpUpdateFile);
                        break;
                    }
                case nameof(OpDeleteFile):
                    {
                        result = DeleteFile(oper as OpDeleteFile);
                        break;
                    }
                default:
                    {
                        result = new NullResult($"{GetType().Name} was invoked with an unsupported operation type ({oper.GetType().Name}).");
                        break;
                    }
            }
            return result;
        }
    }
}
