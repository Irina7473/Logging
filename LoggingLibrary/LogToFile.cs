using System;
using System.IO;
using System.Reflection;

namespace LoggingLibrary
{
    public class LogToFile : ILogger
    {
        public static event Action<LogType, string> Notify;

        private readonly string TotalPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TotalLog.log");

        public LogToFile() { }

        public LogToFile(string path)
        {
            try
            {
                TotalPath = Path.Combine(path, "TotalLog.log");
            }
            catch
            {
                Notify?.Invoke(LogType.Error, "Путь к TotalLog.log не найден.");
            }
        }
                
        public async void RecordToLog(LogType type, string message)
        {
            var text = type + " " + DateTime.Now + " " + Environment.UserName + " "+ message;
            try
            {
                using (StreamWriter writer = new(TotalPath, true))
                { await writer.WriteLineAsync(text); }
            }
            catch (InvalidOperationException)
            {
                Notify?.Invoke(LogType.Error,
                  "Поток в настоящее время используется предыдущей операцией записи.");
            }
            catch (Exception e) { Notify?.Invoke(LogType.Error, e.ToString()); }
        }
              
        public string ReadTheLog()
        {
            string log = "";
            if (File.Exists(TotalPath))
            {
                try
                {
                    using (StreamReader reader = new(TotalPath))
                    { log += reader.ReadToEnd(); }
                }
                catch (OutOfMemoryException)
                {
                    Notify?.Invoke(LogType.Error,
                      "Не хватает памяти для выделения буфера под возвращаемую строку.");
                }
                catch (IOException)
                { Notify?.Invoke(LogType.Error, "Ошибка ввода-вывода."); }
            }
            return log;
        }

        public void ClearLog()
        {
            try
            {
                using (StreamWriter writer = new(TotalPath, false))
                { writer.Write(""); }
            }
            catch (InvalidOperationException)
            {
                Notify?.Invoke(LogType.Error,
                  "Поток в настоящее время используется предыдущей операцией записи.");
            }
            catch (Exception e) { Notify?.Invoke(LogType.Error, e.ToString()); }
        }

    }
}
