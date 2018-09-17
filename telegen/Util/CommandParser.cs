using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace telegen.Util
{
    public class CommandParser
    {

        public static IParsedCommand GetParsedCommand(string switchCharacter = null)
        {
            return Get().Parse(Environment.CommandLine, switchCharacter);
        }

        public static CommandParser Get()
        {
            return new CommandParser();
        }

        public IParsedCommand Parse(string commandText, string switchCharacter = null)
        {
            const string defaultSwitchChar = "--";
            var switchChar = switchCharacter ?? defaultSwitchChar;
            var cmd = new ParsedCommand();
            //commandText = commandText.Trim();
            while (!string.IsNullOrWhiteSpace(commandText))
            {
                //commandText.Dump("Getting token from this commandText");
                var token = GetNextToken(ref commandText);
                if (cmd.Command == null)
                {
                    cmd.Command = token;//.ToUpper();
                }
                else
                {
                    if (token.StartsWith(switchChar))
                    {
                        var switchPair = token.Split('=');
                        if (switchPair.Length == 1)
                        {
                            cmd.SwitchDictionary[switchPair[0].TrimStart(switchChar.ToCharArray())] = true;
                        }
                        else
                        {
                            cmd.SwitchDictionary[switchPair[0].TrimStart(switchChar.ToCharArray())] = switchPair[1];
                        }
                    }
                    else
                    {
                        cmd.Parms.Add(token);
                    }

                }
            }
            return cmd;
        }

        protected string GetNextToken(ref string commandText)
        {
            const char endToken = ' ';
            const char NotInQuotes = char.MinValue;
            const char EscapeChar = '\\';
            const char SQuote = '\'';
            const char DQuote = '"';

            var quoteChar = NotInQuotes;
            var token = string.Empty;
            var data = commandText.TrimStart();
            var endOfTokenFound = false;

            while (!endOfTokenFound)
            {
                var ch = data[0];
                switch (ch)
                {
                    case endToken:
                        if (quoteChar != NotInQuotes)
                        {
                            token = token + ch;
                        }
                        else
                        {
                            endOfTokenFound = true;
                        }
                        break;
                    case EscapeChar:
                        token = token + data[1];
                        data = data.Substring(1);
                        break;
                    case SQuote:                        // You can use either, and one type of quote
                    case DQuote:                        // can contain the other type.
                        if (quoteChar == NotInQuotes)
                        {
                            quoteChar = ch;
                        }
                        else
                        {
                            if (quoteChar == ch)
                            {
                                quoteChar = NotInQuotes;
                            }
                            else
                            {
                                token = token + ch;
                            }
                        }
                        break;
                    default:
                        token = token + ch;
                        break;
                }
                data = data.Substring(1);
                if (data.Length == 0) endOfTokenFound = true;
            }
            commandText = data;
            return token;
        }

    }

    public interface IParsedCommand
    {

        string Command { get; set; }
        List<string> Parms { get; }
        dynamic Switches { get; }
        IDictionary<string, object> SwitchDictionary { get; }
        bool ContainsSwitch(string switchName);

    }

    public class ParsedCommand : IParsedCommand
    {

        public ParsedCommand()
        {
            Parms = new List<string>();
            Switches = new ExpandoObject();
            ;
        }

        public string Command { get; set; }
        public List<string> Parms { get; private set; }
        public dynamic Switches { get; private set; }
        public IDictionary<string, object> SwitchDictionary
        {
            get { return Switches; }
        }

        public bool ContainsSwitch(string switchName)
        {
            return SwitchDictionary.ContainsKey(switchName);
        }

    }
}