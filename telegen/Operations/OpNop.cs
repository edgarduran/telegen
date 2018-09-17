namespace telegen.Operations {
    public class OpNop : Operation {
        public string Command { get; }

        public OpNop(string command) {
            Command = command;
        }
    }
}