using System;
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
    }

}
