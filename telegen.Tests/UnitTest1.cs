using System;
using Akka.Actor;
using Akka.TestKit.Xunit2;
using telegen.Actors;
using telegen.Messages;
using telegen.Messages.Log;
using Xunit;

namespace telegen.Tests
{
    public class TestMessageDefinitions
    {
        [Fact]
        public void TestCreateFileMsg()
        {

        }
    }

    public class TestActors :TestKit {
        [Fact]
        public void TestSpawnInWindows() {
            var a = TestActor;
            var spawner = Sys.ActorOf(Props.Create(() => new SpawnActor(a)));
            spawner.Tell(new SpawnMsg(@"C:\Windows\Notepad.exe"));
            var msg = ExpectMsg<ProcessStartLog>();
            Console.WriteLine(msg.ToString());
            Assert.Equal("tlewis", msg.UserName);            
        }
    }

}
