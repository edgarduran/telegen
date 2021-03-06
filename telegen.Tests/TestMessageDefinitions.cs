using System;
using System.Linq;
using telegen.Messages;
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

        //[Fact]
        //public void TestCreateFileLogMsg()
        //{
        //    var msg = new FileActivityResult(DateTime.UtcNow, "~/file.txt", FileEventType.Create);
        //    Assert.NotEqual(0, msg.ProcessId);
        //    output.WriteLine(msg.ToString());
        //}

        [Fact]
        public void WhatOsAmIOn()
        {
            output.WriteLine(System.Environment.OSVersion.ToString());
            output.WriteLine(System.Environment.OSVersion.Platform.ToString());
        }

        [Fact]
        public void TestOperation()
        {
            dynamic x = new Operation
            {
                Domain = "http",
                Action = "get"
            };

            var targetUrl = "http://www.google.com";

            x.url = targetUrl;
            output.WriteLine(x.ToString());

            Assert.Equal("{\"domain\":\"http\",\"action\":\"get\",\"domain\":\"http\",\"action\":\"get\",\"url\":\"http://www.google.com\"}", x.ToString());

            var value = (x as Operation).Require<string>("url");
            Assert.Equal(value, targetUrl);

            Assert.Null(x.thisPropertyDoesNotExist);

        }

        [Fact]
        public void TestResult()
        {
            var x = new Result();
            x.AsDynamic.filename = "test.txt";
            x.AsDynamic.name = "Terry";

            output.WriteLine(x.ToString());
            
            //x.Params.url = "http://www.google.com";
            //output.WriteLine(x.ToString());

            //Assert.Equal("{\"domain\":\"http\",\"action\":\"get\",\"params\":{\"url\":\"http://www.google.com\"}}", x.ToString());

            //var values = x.Require(p => p.url);
            //Assert.Equal(values.First().ToString(), x.Params.url);

            //Assert.Null(x.Params.thisPropertyDoesNotExist);

        }

        [Fact]
        public void TestOperationResult()
        {
            var op = new Operation
            {
                Domain = "http",
                Action = "get"
            };
            var targetUrl = "http://www.google.com";
            op.AsDynamic.url = targetUrl;

            var x = new Result(op);
            x.AsDynamic.filename = "test.txt";
            x.AsDynamic.name = "Terry";

            output.WriteLine(x.ToString());

            Assert.Equal(op.Domain, x.AsDynamic.Domain);
            Assert.Equal(op.Action, x.AsDynamic.Action);

            //x.Params.url = "http://www.google.com";
            //output.WriteLine(x.ToString());

            //Assert.Equal("{\"domain\":\"http\",\"action\":\"get\",\"params\":{\"url\":\"http://www.google.com\"}}", x.ToString());

            //var values = x.Require(p => p.url);
            //Assert.Equal(values.First().ToString(), x.Params.url);

            //Assert.Null(x.Params.thisPropertyDoesNotExist);

        }

    }

}
