using System;
using System.IO;
using Akka.Actor;
using Akka.TestKit.Xunit2;
using telegen.Actors;
using telegen.Messages;
using telegen.Messages.Log;
using Xunit;
using Xunit.Abstractions;

namespace telegen.Tests
{

    public class TestActors : TestKit
    {

        private readonly ITestOutputHelper output;

        public TestActors(ITestOutputHelper output)
        {
            this.output = output;
        }

        protected bool IsWindows => Environment.OSVersion.Platform == PlatformID.Win32NT;

        [Theory]
        [InlineData(@"C:\Windows\Notepad.exe", "bash")]
        [InlineData(@"C:\Windows\Notepad.exe", "atom")]
        public void TestSpawn(string winFile, string unxFile)
        {
            var a = TestActor;
            var spawner = Sys.ActorOf(Props.Create(() => new SpawnActor(a)));
            var appFile = IsWindows ? winFile : unxFile;
            spawner.Tell(new SpawnMsg(appFile));
            var msg = ExpectMsg<ProcessStartLog>();
            output.WriteLine(msg.ToString());
            Assert.Equal(Environment.UserName, msg.UserName);
        }

        [Fact]
        public void TestFileActorCreate()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = "telegen.testcreatefile.txt";
            var filePath = Path.Combine(folder, filename);
            if (File.Exists(filePath)) File.Delete(filePath);
            try
            {
                var a = TestActor;
                var fileActor = Sys.ActorOf(Props.Create(() => new FileActor(a)), "Creator");

                #region Test Create File
                fileActor.Tell(new CreateFileMsg(folder, filename));

                var createLog = ExpectMsg<ProcessFileActivityLog>();
                output.WriteLine(createLog.ToString());
                Assert.Equal(Environment.UserName, createLog.UserName);
                Assert.Equal(filePath, createLog.FileName);
                Assert.Equal(FileEventType.Create, createLog.FileEventType);
                Assert.True(File.Exists(filePath));
                #endregion

            }
            finally
            {
                if (File.Exists(filePath)) File.Delete(filePath);
            }
        }

        [Fact]
        public void TestFileActorUpdate()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = "telegen.testcreatefile.txt";
            var filePath = Path.Combine(folder, filename);
            if (!File.Exists(filePath)) File.WriteAllText(filePath, string.Empty);
            try
            {
                var a = TestActor;

                #region Test Update File
                var fileActor = Sys.ActorOf(Props.Create(() => new FileActor(a)), "Updater");
                fileActor.Tell(new UpdateFileMsg(folder, filename, "Gotta stick something in here!"));
                var updateLog = ExpectMsg<ProcessFileActivityLog>();
                output.WriteLine(updateLog.ToString());
                Assert.Equal(Environment.UserName, updateLog.UserName);
                Assert.Equal(filePath, updateLog.FileName);
                Assert.Equal(FileEventType.Update, updateLog.FileEventType);
                #endregion

            }
            finally
            {
                if (File.Exists(filePath)) File.Delete(filePath);
            }
        }

        [Fact]
        public void TestFileActorDelete()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = "telegen.testcreatefile.txt";
            var filePath = Path.Combine(folder, filename);
            if (!File.Exists(filePath)) File.WriteAllText(filePath, string.Empty);
            try
            {
                var a = TestActor;
               
                #region Test Delete File
                var fileActor = Sys.ActorOf(Props.Create(() => new FileActor(a)), "Deleter");
                fileActor.Tell(new DeleteFileMsg(folder, filename));
                var deleteLog = ExpectMsg<ProcessFileActivityLog>();
                output.WriteLine(deleteLog.ToString());
                Assert.Equal(Environment.UserName, deleteLog.UserName);
                Assert.Equal(filePath, deleteLog.FileName);
                Assert.Equal(FileEventType.Delete, deleteLog.FileEventType);
                Assert.False(File.Exists(filePath));
                #endregion

            }
            finally
            {
                if (File.Exists(filePath)) File.Delete(filePath);
            }
        }

        [Theory]
        [InlineData("http://images.perseusbooks.com")]
        public void TestNetworkCall(string uri)
        { 
            var client = Sys.ActorOf(Props.Create(() => new NetworkActor(TestActor)), "Network");
            var msg = new NetworkGetData(uri);
            client.Tell(msg);
            var resp = ExpectMsg<WebResp>(TimeSpan.FromSeconds(5));
            output.WriteLine(resp.ToString());

        }

    }

}
