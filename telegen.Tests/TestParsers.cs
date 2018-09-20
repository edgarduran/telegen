using System;using System.IO;using System.Linq;using Akka.TestKit.Xunit2;using telegen.Util;using Xunit;using Xunit.Abstractions;namespace telegen.Tests{    public class TestParsers : TestKit    {        private readonly ITestOutputHelper output;        public TestParsers(ITestOutputHelper output)        {            this.output = output;        }        [Theory]        [InlineData("FILE.CREATE \"~/Tempfile.Txt\"")]        [InlineData("FILE.CREATE ~/Tempfile.Txt")]        [InlineData("FILE.APPEND \"~/Tempfile.Txt\" '\"Today is the first day of the rest of your life.\" -- Unknown'")]        [InlineData("FILE.APPEND ~/Tempfile.Txt '\"Today is the first day of the rest of your life.\" -- Unknown'")]        [InlineData("EXEC atom \"~/Tempfile.Txt\"")]        [InlineData("EXEC atom ~/Tempfile.Txt")]        [InlineData("FILE.DELETE \"~/Tempfile.Txt\"")]        [InlineData("FILE.DELETE ~/Tempfile.Txt")]        [InlineData("NET.GET http://www.google.com")]        [InlineData("WAIT 5000")]        public void TestCommandParser(string command)        {            var parser = new CommandParser();            var cmd = parser.Parse(command);            output.WriteLine(command);            output.WriteLine(cmd.Command);            foreach (var p in cmd.Parms)            {                output.WriteLine(p);            }            output.WriteLine(string.Empty);        }        [Fact]        public void TestScriptingEngine()        {
            //            var oldscript = @"
            //FILE.CREATE ""~/Tempfile.txt""
            //FILE.APPENDLINE ""~/Tempfile.txt"" 'Today is the first day of the rest of your life.'
            //FILE.APPEND ""~/Tempfile.txt"" '             -- '
            //FILE.APPENDLINE ""~/Tempfile.txt"" ' Unknown'
            //EXEC atom  ""~/Tempfile.txt""
            //WAIT 10000
            //FILE.DELETE ""~/Tempfile.txt""
            //NET.GET http://www.google.com
            //";

            //            var script = @"
            //[
            //    {""domain"": ""File"", ""action"": ""Create"", ""params"": {""FileName"": ""~/Tempfile.txt""}},
            //    {""domain"": ""File"", ""action"": ""AppendLine"", ""params"": {""FileName"": ""~/Tempfile.txt"", ""Contents"": ""Today is the first day of the rest of your life.""}},
            //    {""domain"": ""File"", ""action"": ""Append"", ""params"": {""FileName"": ""~/Tempfile.txt"", ""Contents"": ""Today is the first day of the rest of your life.""}},
            //    {""domain"": ""File"", ""action"": ""AppendLine"", ""params"": {""FileName"": ""~/Tempfile.txt"", ""Contents"": ""Today is the first day of the rest of your life.""}},
            //    {""domain"": ""Process"", ""action"": ""Spawn"", ""params"": {""executable"": ""atom"", ""arguments"": ""~/Tempfile.txt""}},
            //    {""domain"": ""System"", ""action"": ""Wait"", ""params"": {""ms"": 4000}},
            //    {""domain"": ""File"", ""action"": ""Delete"", ""params"": {""FileName"": ""~/Tempfile.txt""}},
            //    {""domain"": ""Network"", ""action"": ""Get"", ""params"": {""url"": ""http://www.google.com""}},
            //]
            //";

            var script = @"[    {""domain"": ""File"", ""action"": ""Create"", ""filename"": ""~/Tempfile.txt""},    {""domain"": ""File"", ""action"": ""AppendLine"", ""filename"": ""~/Tempfile.txt"", ""contents"": ""Today is the first day of the rest of your life.""},    {""domain"": ""File"", ""action"": ""Append"", ""filename"": ""~/Tempfile.txt"", ""contents"": ""Today is the first day of the rest of your life.""},    {""domain"": ""File"", ""action"": ""AppendLine"", ""filename"": ""~/Tempfile.txt"", ""contents"": ""Today is the first day of the rest of your life.""},    {""domain"": ""Process"", ""action"": ""Spawn"", ""executable"": ""atom"", ""arguments"": ""~/Tempfile.txt""},    {""domain"": ""Network"", ""action"": ""Get"", ""url"": ""http://www.google.com""},    {""domain"": ""System"", ""action"": ""Wait"", ""ms"": 4000},    {""domain"": ""File"", ""action"": ""Delete"", ""filename"": ""~/Tempfile.txt""}]";            if (Environment.OSVersion.Platform.ToString().Contains("Win")) {                script = script.Replace("atom", "notepad");                script = script.Replace("~/Tempfile.txt", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Tempfile.txt").Replace("\\", "\\\\"));            }            var engine = new ScriptEngine();            var results = engine.ParseAndExecute(script);            foreach (var cmd in results.Where(s => s != null))            {                output.WriteLine(cmd.ToString());            }        }    }}