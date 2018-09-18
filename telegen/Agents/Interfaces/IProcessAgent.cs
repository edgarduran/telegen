using telegen.Operations;
using telegen.Results;

namespace telegen.Agents.Interfaces
{
    public interface IProcessAgent
    {
        SpawnResult Spawn(OpSpawn msg);
    }
}