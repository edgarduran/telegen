using System;

namespace telegen.Messages.Log
{
    public class ProcessFileActivityLog : LogEvent
    {

        public ProcessFileActivityLog (DateTime utcStart, string filename, string fileEventType, string username = null)
        {
            FileName = filename;
            UTCStart = utcStart;
            FileEventType = fileEventType;
            UserName = username ?? Environment.UserName;
        }

        public ProcessFileActivityLog(DateTime utcStart, string fileName, string fileEventType, string userName, string processName, string commandLine, int procId) : base(processName, utcStart, procId)
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
    }

}
