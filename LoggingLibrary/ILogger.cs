using System;

namespace LoggingLibrary
{
    public interface ILogger
    {
        public void RecordToLog(LogType typeevent, string message) { }
        public string ReadTheLog() { return ""; }
        public void ClearLog() { }
    }

    public enum LogType
    {
        Info,
        Warn,
        Debug,
        Error
    }
}
