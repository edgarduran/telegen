using System;
using System.IO;
using System.Linq;
using Akka.Actor;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using telegen.Operations.Results;
using telegen.Util;

namespace telegen
{
    class Program
    {
        static void Main(string[] args)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            Console.WriteLine($"telegen v{version}\n");

            var cmd = CommandParser.GetParsedCommand(); // Reads the command string
            if (HelpRequested(cmd)) return;

            SetDefaults();

            string scriptFile = null;
            string outFile = null;

            switch (cmd.Parms.Count) {
                case 1:
                    scriptFile = cmd.Parms[0];
                    outFile = scriptFile + ".txt";
                    break;
                case 2:
                    scriptFile = cmd.Parms[0];
                    outFile = cmd.Parms[1];
                    break;
                default:
                    Console.WriteLine("\t\tERR: Invalid command line syntax.");
                    ShowHelp();
                    break;
            }

            if (scriptFile == null) return;

            var echoOn = cmd.ContainsSwitch("echo");

            var useCustomLayout = cmd.ContainsSwitch("format");
            string customLayout = null;
            if (useCustomLayout) {
                var customLayoutFile = cmd.Switches.format;
                if (File.Exists(customLayoutFile)) {
                    customLayout = File.ReadAllText(customLayoutFile);
                } else {
                    throw new Exception($"ERR: Could not find requested layout file ({customLayoutFile})");
                }
            }

            if (cmd.ContainsSwitch("clear")) File.Delete(outFile);
            
            ILogger log = new NullLogger(new LogFactory());
            if (useCustomLayout) {
                log = ConfigureNLog(outFile, customLayout);
            } else { 
            var header = $@"[ 
{{ ""source"": ""telegen v{version}"" }}";
                File.WriteAllText(outFile, header);
            }

            var engine = new ScriptEngine();
            foreach (var logEntry in engine.Execute(scriptFile)) {
                if (logEntry == null) continue;
                var text = logEntry.ToString();
                if (useCustomLayout) {
                    var evt = new LogEventInfo(LogLevel.Info, log.Name, logEntry.GetType().Name);
                    logEntry.CopyToDictionary(evt.Properties);
                    log.Info(evt);
                } else {                    
                    File.AppendAllText(outFile, ",\n" + text);
                }

                if (echoOn) Console.WriteLine(text);
            }
            File.AppendAllText(outFile, "\n]");

            Console.WriteLine("\n\nScript complete.\n");
            
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

        private static ILogger ConfigureNLog(string filename, string customLayout) {
            var cfg = new LoggingConfiguration();
            var tgt = new FileTarget("fileWriter") {FileName=filename, Layout = BuildLayout(customLayout)};
            cfg.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, tgt));
            LogManager.Configuration = cfg;
            return LogManager.GetLogger("Reporter");
        }

        private static Layout BuildLayout(string def) {
            var layout = string.Empty;
            var name = string.Empty;
            const char startName = '{';
            const char endName = '}';

            bool inName = false;
            var pos = 0;
            foreach (var c in def.ToCharArray()) {
                pos++;
                switch (c) {
                    case startName:
                        if (inName) {
                            throw new Exception($"Malformed layout near position {pos} : {def.Substring(0, pos)}");
                        } else {
                            inName = true;
                        }
                        break;
                    case endName:
                        if (inName) {
                            inName = false;
                            layout += $"${{event-properties:item={name}}}";
                            name = String.Empty;
                        }
                        else
                        {
                            throw new Exception($"Malformed layout near position {pos} : {def.Substring(0, pos)}");
                        }
                        break;
                    default:
                        if (inName) {
                            name += c;
                        } else {
                            layout += c;
                        }
                        break;

                }
            }

            return layout;
        }

        private static void SetDefaults() {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                //TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
        }

        private static bool HelpRequested(IParsedCommand cmd) {
            if (!cmd.ContainsSwitch("help")) return false;
            ShowHelp();
            return true;

        }


        public static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("   Usage:\n");
            Console.WriteLine("      telegen <scriptfile> [outputfile]  [switches]\n");
            Console.WriteLine("         <scriptfile> -- The name of a text file containing the script to execute. Suggested extension is '.tg'.");
            Console.WriteLine("         [outputfile] -- Optional. Specifies the file name where the results are written. Default is <scriptfile>.txt\n\n");

            Console.WriteLine("   Switches:\n");
            Console.WriteLine("      --help              : Show this help information.");
            Console.WriteLine("      --echo              : Display log output to the screen.\n\n");
            Console.WriteLine("      --clear             : Clear the log before running.\n\n");
            Console.WriteLine("      --format=<filename> : Use a custom format for the output.\n\n");

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
