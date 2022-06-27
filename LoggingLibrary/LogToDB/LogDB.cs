using System;

namespace LoggingLibrary.LogToDB
{
    public class LogDB : ILogger
    {
        public int Id { get; set; }
        public string TypeEvent { get; set; }
        public string DateTimeEvent { get; set; }
        public string User { get; set; }
        public string Message { get; set; }

        public LogDB(int id, string typeevent, string datetimeevent, string user, string message)
        {
            Id = id;
            TypeEvent = typeevent;
            DateTimeEvent = datetimeevent;
            User = user;
            Message = message;
        }

        public LogDB() { }
    }
}
