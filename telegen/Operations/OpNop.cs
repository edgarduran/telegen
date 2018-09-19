namespace telegen.Operations {
    /// <summary>
    /// No operation; do nothing.
    /// </summary>
    /// <seealso cref="telegen.Operations.Operation" />
    public class OpNop : Operation {
        public string Command { get; }

        public OpNop(string command) {
            Command = command;
        }
    }
}