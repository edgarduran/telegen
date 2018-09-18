using telegen.Operations;
using telegen.Operations.Results;

namespace telegen.Agents
{
    public interface INetworkAgent
    {
        NetResult Execute(WebReq req);
    }
}