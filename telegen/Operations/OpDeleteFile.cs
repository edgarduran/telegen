namespace telegen.Operations
{
    /// <summary>
    /// Requests the framework to delete a file. If the file does not exist, the operation is ignored.
    /// </summary>
    /// <seealso cref="telegen.Operations.FileOperation" />
    public class OpDeleteFile : FileOperation
    {
        public OpDeleteFile(string filename) : base(filename)
        {
        }

    }

}
