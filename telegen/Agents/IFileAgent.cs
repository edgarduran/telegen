using telegen.Operations;
using telegen.Operations.Results;

namespace telegen.Agents
{
    public interface IFileAgent
    {
        FileActivityResult CreateFile(OpCreateFile msg);
        FileActivityResult DeleteFile(OpDeleteFile msg);
        FileActivityResult UpdateFile(OpUpdateFile msg);
    }
}