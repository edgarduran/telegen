using Akka.TestKit.Xunit2;
using telegen.Util;
using Xunit;
using Xunit.Abstractions;

namespace telegen.Tests
{
    public class TestParsers : TestKit
    {

        private readonly ITestOutputHelper output;

        public TestParsers(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("FILE CREATE \"~/Tempfile.Txt\"")]
        [InlineData("FILE CREATE ~/Tempfile.Txt")]
        [InlineData("FILE APPEND \"~/Tempfile.Txt\" '\"Today is the first day of the rest of your life.\" -- Unknown'")]
        [InlineData("FILE APPEND ~/Tempfile.Txt '\"Today is the first day of the rest of your life.\" -- Unknown'")]
        [InlineData("EXEC atom \"~/Tempfile.Txt\"")]
        [InlineData("EXEC atom ~/Tempfile.Txt")]
        [InlineData("FILE DELETE \"~/Tempfile.Txt\"")]
        [InlineData("FILE DELETE ~/Tempfile.Txt")]
        [InlineData("NET GET http://www.google.com")]
        public void TestCommandParser(string command)
        {
            var parser = new CommandParser();
            var cmd = parser.Parse(command);
            output.WriteLine(command);
            output.WriteLine(cmd.Command);
            foreach (var p in cmd.Parms)
            {
                output.WriteLine(p);
            }
            output.WriteLine(string.Empty);
        }

    }

}
