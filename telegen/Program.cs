using System;
using Akka.Actor;
using Newtonsoft.Json;
using telegen.Operations.Results;

namespace telegen
{
    class Program
    {
        static void Main(string[] args)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            Console.WriteLine($"telegen v{version}\n");

            var cmd = Util.CommandParser.GetParsedCommand();
            if (!cmd.ContainsSwitch("help"))
            {
                ShowHelp();
                return;
            }


            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { 
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };

            Console.WriteLine(cmd.Command);



            #region Make VS-Windows behave like VS-Mac.
#if DEBUG
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Console.WriteLine("Press ENTER to continue...");
                Console.ReadLine();
            }
#endif
            #endregion
        }

        public static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("   Usage:\n");
            Console.WriteLine("      telegen <scriptfile> [outputfile]  [switches]\n");
            Console.WriteLine("         <scriptfile> -- The name of a text file containing the script to execute. Suggested extension is '.tg'.");
            Console.WriteLine("         [outputfile] -- Optional. Specifies the file name where the results are written. Default is <scriptfile>.txt\n\n");

            Console.WriteLine("   Switches:\n");
            Console.WriteLine("      --help : Show this help information.\n\n");

            Console.WriteLine("   Script Commands:\n");
            Console.WriteLine("      #This is a comment");
            Console.WriteLine("      FILE CREATE <filename>");
            Console.WriteLine("      FILE APPEND <filename>  \"<string to append>\"");
            Console.WriteLine("      FILE APPEND <filename>  '<string to append>'");
            Console.WriteLine("      FILE DELETE <filename>");
            Console.WriteLine("      EXEC <application> <application parameters>");
            Console.WriteLine("      NET GET <url>\n\n");

            Console.WriteLine("   Filenames and string data may be enclosed in single or double-quotes. If single-quotes");
            Console.WriteLine("   are used, then they can include double-quotes, and vise-versa. To comment a line, add");
            Console.WriteLine("   a hashtag (#) to the front of the line.");
            Console.WriteLine();
        }
    }


    public class ActorSystemHost : IDisposable
    {
        ActorSystem _system = null;
        public ActorSystemHost()
        {
            _system = ActorSystem.Create("TeleGen");

        }

        public void Dispose()
        {
            _system.Dispose();
        }
    }


    public static class RootActors
    {
        public static IActorRef CommandParser { get; private set; }
        public static IActorRef Log { get; private set; }
    }
}
