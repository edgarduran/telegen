using System;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace telegen
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Header
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            //Console.WriteLine($"telegen v{version}\n");
            #endregion

            #region Initialization
            if (HelpRequested()) return;

            SetDefaults();

            string script = ReadScriptFromStdIn();

            if (string.IsNullOrWhiteSpace(script)) throw new Exception("No script found in stdin.");

            #endregion

            #region Execute Report

            var engine = new ScriptEngine();
            var rpt = engine.ParseAndExecute(script);
            Console.WriteLine(JsonConvert.SerializeObject(rpt));

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

        private static string ReadScriptFromStdIn()
        {
            StringBuilder script = new StringBuilder();
            var line = string.Empty;
            while ((line = Console.ReadLine()) != null)
            {
                script.AppendLine(line);
            }
            return script.ToString(); 
        }

        private static void SetDefaults() {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        private static bool HelpRequested() {
            if (!Environment.CommandLine.Contains("/help")) return false;
            ShowHelp();
            return true;

        }

        public static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("   Usage:\n");
            Console.WriteLine("      telegen reads from stdin, and writes to stdout. Standard redirection and piping is supported.");


            Console.WriteLine("   Switches:\n");
            Console.WriteLine("      /help              : Show this help information.");

            Console.WriteLine("   Script Example:\n");
            Console.WriteLine(@"
            [
                { ""domain"": ""System"", ""action"": ""Comment"", ""text"": ""Add any properties you want!""},
                { ""domain"": ""File"", ""action"": ""Create"", ""filename"": ""~/Tempfile.txt""},
                { ""domain"": ""File"", ""action"": ""AppendLine"", ""filename"": ""~/Tempfile.txt"", ""contents"": ""Today is the first day of the rest of your life.""},
                { ""domain"": ""File"", ""action"": ""Append"", ""filename"": ""~/Tempfile.txt"", ""contents"": ""Today is the first day of the rest of your life.""},
                { ""domain"": ""File"", ""action"": ""AppendLine"", ""filename"": ""~/Tempfile.txt"", ""contents"": ""Today is the first day of the rest of your life.""},
                { ""domain"": ""Process"", ""action"": ""Spawn"", ""executable"": ""atom"", ""arguments"": ""~/Tempfile.txt""},
                { ""domain"": ""Network"", ""action"": ""Get"", ""url"": ""http://www.google.com""},
                { ""domain"": ""System"", ""action"": ""Wait"", ""ms"": 4000},
                { ""domain"": ""File"", ""action"": ""Delete"", ""filename"": ""~/Tempfile.txt""}
            ]");

            Console.WriteLine();
        }
    }

}
