#define WINDOWS
using System;
using Akka.Actor;
using Newtonsoft.Json;
using telegen.Messages.Log;

namespace telegen
{
    class Program
    {
        static void Main(string[] args)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            Console.WriteLine($"telegen v{version}\n");

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { 
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };

            using (var system = ActorSystem.Create("TeleGen"))
            {

            }
#if WINDOWS
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
#endif
        }
    }


    public static class RootActors
    {
        public static IActorRef CommandParser { get; private set; }
        public static IActorRef Log { get; private set; }
    }
}
