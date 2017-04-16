using System;

namespace LinuxKestrelWebsiteManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgumentsParser argumentsParser = new ArgumentsParser(args);
            argumentsParser.Parse();
            if (!argumentsParser.IsValid)
            {
                DisplayConsoleError(argumentsParser.ValidationMessage);
                return;
            }

            
        }

        private static void DisplayConsoleError(params string[] messages)
        {
            DisplayConsoleMessage(messages, ConsoleColor.Red);
        }

        private static void DisplayConsoleInfo(params string[] messages)
        {
            DisplayConsoleMessage(messages, ConsoleColor.White);
        }

        private static void DisplayConsoleMessage(string[] messages, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            foreach (string message in messages)
            {
                Console.WriteLine(message);
            }
        }
    }
}
