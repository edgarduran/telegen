using telegen.Operations;
using telegen.Results;

namespace telegen.Agents.Interfaces
{
    public interface IFileAgent
    {
        FileActivityResult CreateFile(OpCreateFile msg);
        FileActivityResult DeleteFile(OpDeleteFile msg);
        FileActivityResult UpdateFile(OpUpdateFile msg);
    }
}