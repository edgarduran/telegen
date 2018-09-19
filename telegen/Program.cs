using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using telegen.Agents;
using telegen.Agents.Interfaces;
using telegen.Util;

namespace telegen
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Header
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            Console.WriteLine($"telegen v{version}\n");
            #endregion

            #region Initialization
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
            ReportLayout customLayout = null;
            IReportAgent rpt;
            dynamic header = null;
            if (useCustomLayout) {
                var customLayoutFile = cmd.Switches.format;
                if (File.Exists(customLayoutFile)) {
                    //customLayout = File.ReadAllText(customLayoutFile);
                    customLayout = ReportLayout.Open(customLayoutFile);
                } else {
                    throw new Exception($"ERR: Could not find requested layout file ({customLayoutFile})");
                }
                rpt = new CustomReportAgent(outFile, customLayout);
            } else {
                rpt = new JSONReportAgent(outFile);
                header = new ExpandoObject();
                header.rundate = DateTime.UtcNow;
                header.version = version;
            }
            #endregion

            #region Execute Report

            if (cmd.ContainsSwitch("clear") && File.Exists(outFile)) File.Delete(outFile);
            
            rpt.EmitHeader(header);

            var engine = new ScriptEngine();
            foreach (var logEntry in engine.Execute(scriptFile)) {
                if (logEntry == null) continue;
                rpt.EmitDetailLine(logEntry);
                if (echoOn) Console.WriteLine(logEntry.ToString());
            }

            rpt.EmitFooter();

            #endregion
            
            #region Make VS-Windows behave like VS-Mac.
#if DEBUG
            if (Environment.OSVersion.Platform.ToString().Contains("Win"))
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
#endif
            #endregion
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

            Console.WriteLine("   Script Command Examples:\n");
            Console.WriteLine("      # This is a comment");
            Console.WriteLine("      FILE CREATE <filename>");
            Console.WriteLine("      FILE APPEND <filename>  \"<string to append>\"");
            Console.WriteLine("      FILE APPENDLINE <filename>  '<string to append>'");
            Console.WriteLine("      FILE DELETE <filename>");
            Console.WriteLine("      EXEC <application> <application parameters>");
            Console.WriteLine("      NET GET <url>\n\n");
            Console.WriteLine("      WAIT <milliseconds>\n\n");

            Console.WriteLine("   Filenames and string data may be enclosed in single or double-quotes. If single-quotes");
            Console.WriteLine("   are used, then they can include double-quotes, and vise-versa. To comment a line, add");
            Console.WriteLine("   a hashtag (#) to the front of the line.");
            Console.WriteLine();
        }
    }


}
