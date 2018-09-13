using Newtonsoft.Json;

namespace telegen.Messages
{
    public abstract class MsgBase
    {
        protected MsgBase()
        {
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }

}
