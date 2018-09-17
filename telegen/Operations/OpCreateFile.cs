namespace telegen.Operations
{
    public class OpCreateFile : FileOperation
    {
        public OpCreateFile(string fullname) : base(fullname) { }

        public OpCreateFile(string path, string filename) : base(path, filename)
        {
        }

    }

}
