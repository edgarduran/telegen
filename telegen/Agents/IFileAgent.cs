using telegen.Messages;
using telegen.Messages.Log;

namespace telegen.Agents
{
    public interface IFileAgent
    {
        FileActivity CreateFile(CreateFileMsg msg);
        FileActivity DeleteFile(DeleteFileMsg msg);
        FileActivity UpdateFile(UpdateFileMsg msg);
    }
}