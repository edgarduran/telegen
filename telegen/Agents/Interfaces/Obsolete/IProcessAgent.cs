using telegen.Operations;
using telegen.Results;

namespace telegen.Agents.Interfaces
{
    /// <summary>
    /// Starts applications
    /// </summary>
    public interface IProcessAgent
    {
        /// <summary>
        /// Spawns the application specified in the operation.
        /// </summary>
        /// <param name="msg">The operation describing the application to be started.</param>
        /// <returns>The results of the operation, including the process name and id number, or null if no action was taken.</returns>
        SpawnResult Spawn(OpSpawn msg);
    }
}