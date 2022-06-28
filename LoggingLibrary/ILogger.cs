using System;

namespace LoggingLibrary
{
    /// <summary>
    /// Интерфейс для журналирования
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Запись сообщений в выбранный источник
        /// </summary>
        /// <param name="typeevent">Тип события</param>
        /// <param name="message">Сообщение</param>
        public void RecordToLog(LogType typeevent, string message) { }
        /// <summary>
        /// Чтение всех сообщений из выбранного источника
        /// </summary>
        /// <returns>Строка, которая включает все записи</returns>
        public string ReadTheLog() { return ""; }
        /// <summary>
        /// Очищение выбранного источника с записями
        /// </summary>
        public void ClearLog() { }
    }

    /// <summary>
    /// Перечисление вариантов типов сообщений
    /// </summary>
    public enum LogType
    {
        Info,
        Warn,
        Debug,
        Error
    }
}
