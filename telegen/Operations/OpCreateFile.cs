namespace telegen.Operations
{

    /// <summary>
    /// Requests the framework to create a file. If the file exists, the operation is ignored.
    /// </summary>
    /// <seealso cref="telegen.Operations.FileOperation" />
    public class OpCreateFile : FileOperation
    {
        public OpCreateFile(string fullname) : base(fullname) { }

        public OpCreateFile(string path, string filename) : base(path, filename)
        {
        }

    }

}
