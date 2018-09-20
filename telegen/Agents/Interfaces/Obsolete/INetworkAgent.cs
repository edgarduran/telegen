using telegen.Operations;
using telegen.Results;

namespace telegen.Agents.Interfaces
{
    /// <summary>
    /// Performs network-related operations.
    /// </summary>
    public interface INetworkAgent
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="req">The web request.</param>
        /// <returns>The results of the operation.</returns>
        NetResult Execute(WebReq req);
    }
}