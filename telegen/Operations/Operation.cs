using Newtonsoft.Json;

namespace telegen.Operations
{
    public abstract class Operation
    {
        protected Operation()
        {
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }

}
