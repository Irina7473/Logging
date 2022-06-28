using System;
using System.IO;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace LoggingLibrary.LogToDB
{
    public class LogToDB : ILogger
    {
        public static event Action<LogType, string> Notify;

        private static FileStream file;
        private static Config config;
        private string connectionString;
        private static SqliteConnection _connection;
        private static SqliteCommand _query;

        public LogToDB()
        {
            file = new("configLog.json", FileMode.Open);
            config = JsonSerializer.DeserializeAsync<Config>(file).Result;
            connectionString = $"Data Source={config.DataSource};Mode={config.Mode};";
            _connection = new SqliteConnection(connectionString);
            _query = new SqliteCommand { Connection = _connection };
        }

        public LogToDB(string patch)
        {
            connectionString = $"Data Source={patch};Mode=ReadWrite;";
            _connection = new SqliteConnection(connectionString);
            _query = new SqliteCommand { Connection = _connection };
        }

        private void Open()
        {
            try
            {
                _connection.Open();
            }
            catch (InvalidOperationException)
            {
                Notify?.Invoke(LogType.Error, "Ошибка открытия базы данных");
            }
            catch (SqliteException)
            {
                Notify?.Invoke(LogType.Error, "Подключаемся к уже открытой базе данных");
            }
            catch (Exception)
            {
                Notify?.Invoke(LogType.Error, "Путь к базе данных не найден");
            }
        }

        private void Close()
        {
            _connection.Close();
        }

        private SqliteDataReader SelectQuery(string sql)
        {
            _query.CommandText = sql;
            var result = _query.ExecuteReader();
            return result;
        }

        public void RecordToLog(LogType type, string message)
        {
            _connection.Open();
            _query.CommandText = $"INSERT INTO tab_log_DB (type_event, date_time_event, user, message)" +
                    $"VALUES ('{type}', '{DateTime.Now}', '{Environment.UserName}', '{message}')";
            _query.ExecuteNonQuery();
            _connection.Close();
        }

        public string ReadTheLog()
        {
            _connection.Open();
            var sql = "SELECT * FROM tab_log_DB";
            using var result = SelectQuery(sql);

            if (!result.HasRows)
            {
                Notify?.Invoke(LogType.Info, "Нет записей в базе данных");
                return "Нет данных";
            }
            else
            {
                var totals = new List<LogDB>();
                while (result.Read())
                {
                    var total = new LogDB
                    {
                        Id = result.GetInt32(0),
                        TypeEvent = result.GetString(1),
                        DateTimeEvent = result.GetString(2),
                        User = result.GetString(3),
                        Message = result.GetString(4)
                    };
                    totals.Add(total);
                }
                string log = "";
                foreach (var total in totals)
                    log += total.TypeEvent + " " + total.DateTimeEvent + " "
                        + total.User + " " + total.Message + "\n";
                return log;
            }
        }

        public void ClearLog()
        {
            _connection.Open();
            _query.CommandText = "DELETE FROM tab_log_DB";
            _query.ExecuteNonQuery();
            _connection.Close();
        }
    }
}
