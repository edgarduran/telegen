using telegen.Actors;

namespace telegen.Agents
{
    public interface INetworkAgent
    {
        WebResp Execute(WebReq req);
    }
}