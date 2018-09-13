namespace telegen.Messages
{
    public class DeleteFileMsg : FileMessage
    {
        public DeleteFileMsg(string path, string filename) : base(path, filename)
        {
        }

    }

}
