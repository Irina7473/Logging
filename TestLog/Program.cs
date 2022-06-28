using System;
using System.Threading;
using LoggingLibrary;
using LoggingLibrary.LogToDB;

namespace TestLog
{
    class Program
    {
        static void Main(string[] args)
        {
            //Проверка записи в консоль
            Console.WriteLine("Создаю лог в консоль");
            var log2 = new LogToConsole();
            Console.WriteLine("---------------------------");

            Console.WriteLine("Запись в лог");
            log2.RecordToLog(LogType.Info, "Запись в лог началась");
            log2.RecordToLog(LogType.Warn, "Проверка лога");
            Console.WriteLine("Очищаю лог");
            Thread.Sleep(3000);
            log2.ClearLog();
            log2.RecordToLog(LogType.Error, "Проверь ошибки");
            log2.RecordToLog(LogType.Debug, "Распаковываю");
            Console.WriteLine(log2.ReadTheLog());
            Console.WriteLine("---------------------------");

            //Проверка записи в файл
            Console.WriteLine("Создаю лог в файл");
            var log1 = new LogToFile();            
            Console.WriteLine(log1.ReadTheLog());
            Console.WriteLine("---------------------------");

            Console.WriteLine("Очищаю лог");
            log1.ClearLog();
            Console.WriteLine(log1.ReadTheLog());
            Console.WriteLine("---------------------------");

            Console.WriteLine("Запись в лог");
            log1.RecordToLog(LogType.Info, "Запись в лог началась");
            log1.RecordToLog(LogType.Warn, "Проверка лога");
            log1.RecordToLog(LogType.Error, "Проверь ошибки");
            log1.RecordToLog(LogType.Debug, "Распаковываю");
            Console.WriteLine(log1.ReadTheLog());
            Console.WriteLine("---------------------------");
                       
            //Проверка записи в базу данных
            Console.WriteLine("Создаю лог в БД");

            //Здесь пока не работает создание лога и открытие
            string patch = @"J:\Test tasks\Logging\LoggingLibrary\LogToDB\logger_db.sqlite";
            var log3 = new LogToDB(patch);
            Console.WriteLine(log3.ReadTheLog());
            Console.WriteLine("---------------------------");

            Console.WriteLine("Очищаю лог");
            log3.ClearLog();
            Console.WriteLine(log3.ReadTheLog());
            Console.WriteLine("---------------------------");

            Console.WriteLine("Запись в лог");
            log3.RecordToLog(LogType.Info, "Запись в лог началась");
            log3.RecordToLog(LogType.Warn, "Проверка лога");
            log3.RecordToLog(LogType.Error, "Проверь ошибки");
            log3.RecordToLog(LogType.Debug, "Распаковываю");
            Console.WriteLine(log3.ReadTheLog());
            Console.WriteLine("---------------------------");
        }

        

    }    
}
