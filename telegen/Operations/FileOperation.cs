using System;
namespace telegen.Operations
{
    public abstract class FileOperation : Operation
    {
        protected FileOperation(string fullname)
        {
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
