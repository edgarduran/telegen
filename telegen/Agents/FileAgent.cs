using System;
using System.IO;
using System.Linq;
using telegen.Agents.Interfaces;
using telegen.Messages;
using telegen.Results;

namespace telegen.Agents
{

    /// <summary>
    /// Exercises the File domain.
    /// <para>
    /// Domain-specific fields:
    /// </para>
    /// <para>
    /// <list type="bullet">
    ///     <item>Event timestamp (when it can be pulled from the file)</item>
    ///     <item>Full name of file</item>
    ///     <item>Activity descriptor (Create, Update, Delete)</item>
    /// </list>
    /// </para>
    /// </summary>
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
                        result = null;
                        break;
                    }
            }
            return result;
        }

        protected Result CreateFile(Operation msg)
        {
            var fn = NormalizeFileName(msg.Require<string>("filename")); 
            File.WriteAllText(fn, string.Empty);
            var fi = new FileInfo(fn);

            dynamic r = new Result(msg);
            (r as Result).Clear("timeStampUtc"); // We're gonna read this from the file.
            r.timeStampUtc = fi.CreationTimeUtc;
            r.fileEventType = FileEventType.Create;
            r.fileName = fn;

            return r;
        }

        protected Result AppendToFile(Operation msg, bool postAppendNewLine)
        {
            var (fn, contents) = msg.Require<string, string>("filename", "contents");
            fn = NormalizeFileName(fn);
            if (postAppendNewLine) contents += Environment.NewLine;
            if (File.Exists(fn))
            {
                File.AppendAllText(fn, contents);
                var fi = new FileInfo(fn);

                dynamic r = new Result(msg);
                (r as Result).Clear("timeStampUtc"); // We're gonna read this from the file.
                r.timeStampUtc = fi.LastWriteTimeUtc;
                r.fileEventType = FileEventType.Update;
                r.fileName = fn;

                return r;
            }
            return null;
        }

        protected Result DeleteFile(Operation msg)
        {
            var fn = NormalizeFileName(msg.Require<string>("filename"));
            if (File.Exists(fn))
            {
                File.Delete(fn);

                dynamic r = new Result(msg);
                r.fileEventType = FileEventType.Delete;
                r.fileName = fn;

                return r;

            }
            return null;
        }

        protected static string NormalizeFileName(string filename)
        {
            var results = filename
                .Replace("~", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            results = Path.GetFullPath(results); // Document requests the full path to the file. Try to get it here.
            return results;
        }

    }
}
