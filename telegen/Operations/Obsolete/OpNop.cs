namespace telegen.Operations {
    /// <summary>
    /// No operation; do nothing.
    /// </summary>
    /// <seealso cref="telegen.Operations.OldOperation" />
    public class OpNop : OldOperation {
        public string Command { get; }

        public OpNop(string command) {
            Command = command;
        }
    }
}