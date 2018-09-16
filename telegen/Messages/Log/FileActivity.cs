using System;
using System.Collections.Generic;

namespace telegen.Messages.Log
{
    public class FileActivity : LogEvent
    {

        public FileActivity (DateTime utcStart, string filename, string fileEventType, string username = null)
        {
            FileName = filename;
            UTCStart = utcStart;
            FileEventType = fileEventType;
            UserName = username ?? Environment.UserName;
        }

        public FileActivity(DateTime utcStart, string fileName, string fileEventType, string userName, string processName, string commandLine, int procId) : base(processName, utcStart, procId)
        {
            FileName = fileName;
            FileEventType = fileEventType;
            UserName = userName;
            CommandLine = commandLine;
        }

        public string FileName { get; }
        public string FileEventType { get; }
        public string UserName { get; }
        public string CommandLine { get; }

        public override void CopyToDictionary(IDictionary<object, object> d)
        {
            d[nameof(FileName)] = FileName;
            d[nameof(FileEventType)] = FileEventType;
            d[nameof(UserName)] = UserName;
            d[nameof(CommandLine)] = CommandLine;
            base.CopyToDictionary(d);
        }
    }

}
