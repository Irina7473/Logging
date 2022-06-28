using System;
using System.IO;
using System.Reflection;

namespace LoggingLibrary
{
    /// <summary>
    /// Класс для журналирования в файл
    /// </summary>
    public class LogToFile : ILogger
    {
        public static event Action<LogType, string> Notify;

        private readonly string TotalPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TotalLog.log");

        /// <summary>
        /// Конструктор лога в файл без параметров
        /// </summary>
        public LogToFile() { }

        /// <summary>
        /// Конструктор лога в файл по заданному пути
        /// </summary>
        /// <param name="path">Путь к файлу</param>
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
                
        /// <summary>
        /// Запись сообщений в файл
        /// </summary>
        /// <param name="type">Тип события</param>
        /// <param name="message">Сообщение</param>
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
              
        /// <summary>
        /// Чтение всех сообщений из файла
        /// </summary>
        /// <returns>Строка, которая включает все записи</returns>
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

        /// <summary>
        /// Очищение файла
        /// </summary>
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
