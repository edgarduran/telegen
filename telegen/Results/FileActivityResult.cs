using System;
using System.Collections.Generic;

namespace telegen.Results
{
    public class FileActivityResult : Result
    {

        public FileActivityResult (DateTime utcStart, string filename, string fileEventType, string username = null)
        {
            FileName = filename;
            UTCStart = utcStart;
            FileEventType = fileEventType;
            UserName = username ?? Environment.UserName;
        }

        public FileActivityResult(DateTime utcStart, string fileName, string fileEventType, string userName, string processName, string commandLine, int procId) : base(processName, utcStart, procId)
        {
            FileName = fileName;
            FileEventType = fileEventType;
            UserName = userName;
            CommandLine = commandLine;
        }

        public string FileName { get; }
        public string FileEventType { get; }
 
    }

}
