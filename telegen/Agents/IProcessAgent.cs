using telegen.Operations;
using telegen.Operations.Results;

namespace telegen.Agents
{
    public interface IProcessAgent
    {
        SpawnResults Spawn(OpSpawn msg);
    }
}