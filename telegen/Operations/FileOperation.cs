using System;
namespace telegen.Operations
{
    /// <summary>
    /// Represents a generic operation targeting the file system.
    /// </summary>
    /// <seealso cref="telegen.Operations.Operation" />
    public abstract class FileOperation : Operation
    {
        protected FileOperation(string fullname)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                fullname = fullname.Replace("/", "\\");
            }

            FullName = fullname.Replace("~", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
        }

        protected FileOperation(string path, string fileName)
        {
            FullName = System.IO.Path.Combine(path, fileName);
        }
        //public string Path { get; }
       //public string FileName { get; }

        public string FullName { get; }
    }

}
