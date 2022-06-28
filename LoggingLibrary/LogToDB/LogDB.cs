using System;

namespace LoggingLibrary.LogToDB
{
    /// <summary>
    /// Класс - модель базы данных
    /// </summary>
    public class LogDB : ILogger
    {
        public int Id { get; set; }
        public string TypeEvent { get; set; }
        public string DateTimeEvent { get; set; }
        public string User { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Конструктор модели лога из БД с параметрами
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="typeevent">Тип события</param>
        /// <param name="datetimeevent">Время записи</param>
        /// <param name="user">Пользователь</param>
        /// <param name="message">Сообщение</param>
        public LogDB(int id, string typeevent, string datetimeevent, string user, string message)
        {
            Id = id;
            TypeEvent = typeevent;
            DateTimeEvent = datetimeevent;
            User = user;
            Message = message;
        }

        /// <summary>
        /// Конструктор модели лога из БД без параметров
        /// </summary>
        public LogDB() { }
    }
}
