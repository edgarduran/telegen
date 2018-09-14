namespace telegen.Messages
{
    public abstract class FileMessage : MsgBase
    {
        public FileMessage(string path, string fileName)
        {
            Path = path;
            FileName = fileName;
        }
        public string Path { get; }
        public string FileName { get; }

        public string FullName => System.IO.Path.Combine(Path, FileName);
    }

}
