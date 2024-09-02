using System;
using System.IO;

namespace CustomExceptions
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
                // Attempt to log to database
                LogToDatabase(ex);
            }
            catch (Exception dbEx)
            {
                // If logging to the database fails, log to a file
                LogToFile(ex, dbEx);
            }
        }

        private void LogToDatabase(Exception ex)
        {
            // Your database logging logic here
            // For example, inserting the exception details into an `ErrorLogs` table

            // Simulate potential failure in logging to database
            throw new Exception("Simulated database logging failure.");
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
                // Handle any potential errors while logging to a file, such as access issues.
                Console.WriteLine("Failed to log exception to file.");
                Console.WriteLine(fileEx.Message);
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

            // Log the exception
            _loggerService.LogError(this);
        }
    }

    public class ImageAddingException : CustomException
    {
        public ImageAddingException(string explanation, ILoggerService loggerService)
            : base("ImageAddingException", "Could not add image.", explanation, loggerService)
        {
        }
    }

    public class NotFoundInDbException : CustomException
    {
        public NotFoundInDbException(string explanation, ILoggerService loggerService)
            : base("NotFoundInDbException", "Failed to get item from database.", explanation, loggerService)
        {
        }
    }

    public class ModelStateException : CustomException
    {
        public ModelStateException(string explanation, ILoggerService loggerService)
            : base("ModelStateException", "Model state is invalid.", explanation, loggerService)
        {
        }
    }

    public class UnauthorizedUserException : CustomException
    {
        public UnauthorizedUserException(string explanation, ILoggerService loggerService)
            : base("UnauthorizedUserException", "User is not authorized to perform this action.", explanation, loggerService)
        {
        }
    }
}
