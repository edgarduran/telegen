using telegen.Operations;
using telegen.Results;

namespace telegen.Agents.Interfaces
{
    public interface INetworkAgent
    {
        NetResult Execute(WebReq req);
    }
}