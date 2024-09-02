using System;
using System.IO;

namespace Common.CustomExceptions
{
    public interface ILoggerService
    {
        void LogError(Exception ex);
    }

    public class LoggerService : ILoggerService
    {
        private readonly string _logFilePath;

        public LoggerService(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void LogError(Exception ex)
        {
            try
            {
                LogToDatabase(ex);
            }
            catch (Exception dbEx)
            {
                LogToFile(ex, dbEx);
            }
        }

        private void LogToDatabase(Exception ex)
        {
            try
            {
                // Simulate potential failure in logging to database
                throw new Exception("Simulated database logging failure.");
            }
            catch (Exception dbEx)
            {
                LogToFile(ex, dbEx);
            }
        }

        private void LogToFile(Exception originalEx, Exception dbEx)
        {
            try
            {
                using (var writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine("--------------------------------------------------");
                    writer.WriteLine($"Time: {DateTime.Now}");
                    writer.WriteLine($"Original Exception: {originalEx.GetType().Name}");
                    writer.WriteLine($"Message: {originalEx.Message}");
                    writer.WriteLine($"StackTrace: {originalEx.StackTrace}");
                    writer.WriteLine($"Database Logging Exception: {dbEx.Message}");
                    writer.WriteLine("--------------------------------------------------");
                    writer.WriteLine();
                }
            }
            catch (Exception fileEx)
            {
                throw new FileLoggingException("Failed to log exception to file.", fileEx);
            }
        }
    }

    public class CustomException : Exception
    {
        public string Name { get; }
        public string Description { get; }
        public string Explanation { get; }
        public DateTime Time { get; }

        private readonly ILoggerService _loggerService;

        public CustomException(string name, string description, string explanation, ILoggerService loggerService)
            : base($"{name}: {description}")
        {
            Name = name;
            Description = description;
            Explanation = explanation;
            Time = DateTime.Now;
            _loggerService = loggerService;

            _loggerService.LogError(this);
        }
    }

    public class DatabaseLoggingException : Exception
    {
        public DatabaseLoggingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class FileLoggingException : Exception
    {
        public FileLoggingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class DatabaseAccessException : CustomException
    {
        public DatabaseAccessException(string message, ILoggerService loggerService)
            : base("DatabaseAccessException", "Access to the database failed.", message, loggerService)
        {
        }
    }

    public class ValidationException : CustomException
    {
        public ValidationException(string message, ILoggerService loggerService)
            : base("ValidationException", "Validation failed.", message, loggerService)
        {
        }
    }
}
