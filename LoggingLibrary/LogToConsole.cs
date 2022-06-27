using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingLibrary
{
    public class LogToConsole : ILogger
    {
        public static event Action<LogType, string> Notify;

        public LogToConsole() { }              

        public async void RecordToLog(LogType type, string message)
        {
            var text = type + " " + DateTime.Now + " " + Environment.UserName + " " + message;
            Output(type, message);
        }

        public string ReadTheLog()
        {
            return null;
        }

        public void ClearLog()
        {
            Console.Clear();
        }


        static void Output(LogType type, string message)
        {
            switch (type)
            {
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogType.Warn:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case LogType.Debug:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.WriteLine($"{type} {message}");
            Console.ResetColor();
        }

    }
}
