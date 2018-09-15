using telegen.Messages;
using telegen.Messages.Log;

namespace telegen.Agents
{
    public interface IFileAgent
    {
        ProcessFileActivityLog CreateFile(CreateFileMsg msg);
        ProcessFileActivityLog DeleteFile(DeleteFileMsg msg);
        ProcessFileActivityLog UpdateFile(UpdateFileMsg msg);
    }
}