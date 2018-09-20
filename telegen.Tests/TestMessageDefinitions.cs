using System;
using System.Linq;
using telegen.Operations;
using telegen.Results;
using Xunit;
using Xunit.Abstractions;

namespace telegen.Tests
{
    public class TestMessageDefinitions
    {
        private readonly ITestOutputHelper output;

        public TestMessageDefinitions(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestCreateFileLogMsg()
        {
            var msg = new FileActivityResult(DateTime.UtcNow, "~/file.txt", FileEventType.Create);
            Assert.NotEqual(0, msg.ProcessId);
            output.WriteLine(msg.ToString());
        }

        [Fact]
        public void WhatOsAmIOn()
        {
            output.WriteLine(System.Environment.OSVersion.ToString());
            output.WriteLine(System.Environment.OSVersion.Platform.ToString());
        }

        [Fact]
        public void TestOperation()
        {
            Assert.True(false);
            var x = new Operation
            {
                Domain = "http",
                Action = "get"
            };
            //x.Params.url = "http://www.google.com";
            //output.WriteLine(x.ToString());

            //Assert.Equal("{\"domain\":\"http\",\"action\":\"get\",\"params\":{\"url\":\"http://www.google.com\"}}", x.ToString());

            //var values = x.Require(p => p.url);
            //Assert.Equal(values.First().ToString(), x.Params.url);

            //Assert.Null(x.Params.thisPropertyDoesNotExist);

        }
    }

}
