namespace telegen.Operations
{
    /// <summary>
    /// Retrieves data from a server using http get.
    /// </summary>
    /// <seealso cref="telegen.Operations.NetworkMessage" />
    public class OpNetGet : NetworkMessage
    {
        public OpNetGet(string uri) : base(uri)
        {
        }
    }

}
