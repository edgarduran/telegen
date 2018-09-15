using telegen.Messages;
using telegen.Messages.Log;

namespace telegen.Agents
{
    public interface IProcessAgent
    {
        ProcessStartLog Spawn(SpawnMsg msg);
    }
}