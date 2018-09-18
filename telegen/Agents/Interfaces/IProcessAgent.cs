using telegen.Operations;
using telegen.Results;

namespace telegen.Agents.Interfaces
{
    public interface IProcessAgent
    {
        SpawnResults Spawn(OpSpawn msg);
    }
}