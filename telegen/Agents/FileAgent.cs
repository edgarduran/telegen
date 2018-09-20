using System;
using System.IO;
using System.Linq;
using telegen.Agents.Interfaces;
using telegen.Operations;
using telegen.Results;

namespace telegen.Agents
{
    public class FileAgent : Agent //, IFileAgent
    {
        public override Result Execute(Operation oper)
        {
            Guard(oper, "Create", "Append", "AppendLine", "Delete");
            Result result = null;
            switch (oper.Action.ToLowerInvariant())
            {
                case "create":
                    {
                        result = CreateFile(oper);
                        break;
                    }
                case "append":
                    {
                        result = AppendToFile(oper, false);
                        break;
                    }
                case "appendline":
                    {
                        result = AppendToFile(oper, true);
                        break;
                    }
                case "delete":
                    {
                        result = DeleteFile(oper);
                        break;
                    }
                default:
                    {
                        result = new NullResult($"{GetType().Name} was invoked with an unsupported action ({oper.Action}).");
                        break;
                    }
            }
            return result;
        }

        protected FileActivityResult CreateFile(Operation msg)
        {
            var fn = NormalizeFileName(msg.Require<string>("filename")); 
            File.WriteAllText(fn, string.Empty);
            var fi = new FileInfo(fn);
            return new FileActivityResult(fi.CreationTimeUtc, fn, FileEventType.Create, Environment.UserName);
        }

        protected FileActivityResult AppendToFile(Operation msg, bool postAppendNewLine)
        {
            var (fn, contents) = msg.Require<string, string>("filename", "contents");
            fn = NormalizeFileName(fn);
            if (postAppendNewLine) contents += Environment.NewLine;
            if (File.Exists(fn))
            {
                File.AppendAllText(fn, contents);
                var fi = new FileInfo(fn);                
                return new FileActivityResult(fi.LastWriteTimeUtc, fn, FileEventType.Update, Environment.UserName);
            }
            return null;
        }

        protected FileActivityResult DeleteFile(Operation msg)
        {
            //TODO: What do I do if the requested event is not performed?
            var fn = NormalizeFileName(msg.Require<string>("filename"));
            if (File.Exists(fn))
            {
                File.Delete(fn);
                return new FileActivityResult(DateTime.UtcNow, fn, FileEventType.Delete, Environment.UserName);

            }
            return null;
        }

        protected static string NormalizeFileName(string filename)
        {
            var results = filename
                .Replace("~", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            return results;
        }

    }
}
