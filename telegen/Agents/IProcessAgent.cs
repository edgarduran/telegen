using telegen.Messages;
using telegen.Messages.Log;

namespace telegen.Agents
{
    public interface IProcessAgent
    {
        Spawn Spawn(SpawnMsg msg);
    }
}