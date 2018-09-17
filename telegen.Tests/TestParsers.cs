﻿using Akka.TestKit.Xunit2;
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
        [InlineData("FILE.CREATE \"~/Tempfile.Txt\"")]
        [InlineData("FILE.CREATE ~/Tempfile.Txt")]
        [InlineData("FILE.APPEND \"~/Tempfile.Txt\" '\"Today is the first day of the rest of your life.\" -- Unknown'")]
        [InlineData("FILE.APPEND ~/Tempfile.Txt '\"Today is the first day of the rest of your life.\" -- Unknown'")]
        [InlineData("EXEC atom \"~/Tempfile.Txt\"")]
        [InlineData("EXEC atom ~/Tempfile.Txt")]
        [InlineData("FILE.DELETE \"~/Tempfile.Txt\"")]
        [InlineData("FILE.DELETE ~/Tempfile.Txt")]
        [InlineData("NET.GET http://www.google.com")]
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

        [Fact]
        public void TestScriptTranslators ()
        {
            var script = @"
FILE.CREATE ""~/Tempfile.Txt""
FILE.APPEND ""~/Tempfile.Txt"" 'Now is the first day of the rest of your life. -- Unknown'
EXEC atom  ""~/Tempfile.Txt""
FILE.DELETE ""~/Tempfile.Txt""
NET.GET http://www.google.com
            ";

            var xlator = new ScriptTranslator();
            var lines = script.Split(System.Environment.NewLine.ToCharArray());
            var results = xlator.Translate(lines); 
            foreach (var cmd in results)
            {
                output.WriteLine(cmd.ToString());
            }
        }


        [Fact]
        public void TestScriptingEngine()
        {
            var script = @"
FILE.CREATE ""~/Tempfile.Txt""
FILE.APPEND ""~/Tempfile.Txt"" 'Now is the first day of the rest of your life. -- Unknown'
EXEC atom  ""~/Tempfile.Txt""
FILE.DELETE ""~/Tempfile.Txt""
NET.GET http://www.google.com
            ";

            var engine = new ScriptEngine();
            var lines = script.Split(System.Environment.NewLine.ToCharArray());
            var results = engine.Execute(lines);
            foreach (var cmd in results)
            {
                output.WriteLine(cmd.ToString());
            }
        }

    }

}
