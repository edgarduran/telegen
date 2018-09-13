namespace telegen.Messages
{
    public class CreateFileMsg : FileMessage
    {
        public CreateFileMsg(string path, string filename, bool createPaths = false) : base(path, filename)
        {
            CreatePaths = createPaths;
        }

        public bool CreatePaths { get; }
    }

}
