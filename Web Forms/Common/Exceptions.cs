using System;

namespace Common.CustomExceptions
{
    // Custom base exception for better readability
    public class CustomException : Exception
    {
        public string Name { get; }
        public string Description { get; }
        public string Details { get; }
        public DateTime Time { get; }

        public CustomException(string name, string description, string details)
            : base($"{name}: {description}")
        {
            Name = name;
            Description = description;
            Details = details;
            Time = DateTime.Now;
        }
    }

    public class FileLoggingException : CustomException
    {
        public FileLoggingException(string message, Exception innerException)
            : base("FileLoggingException", message, innerException?.Message)
        {
        }
    }

    public class DatabaseAccessException : CustomException
    {
        public DatabaseAccessException(string message, Exception innerException)
            : base("DatabaseAccessException", message, innerException?.Message)
        {
        }
    }

    public class ValidationException : CustomException
    {
        public ValidationException(string message, Exception innerException)
            : base("ValidationException", message, innerException?.Message)
        {
        }
    }

    public class EmployeeIDNotFoundInDbException : CustomException
    {
        public EmployeeIDNotFoundInDbException(string message)
            : base("EmployeeIDNotFoundInDbException", "Employee ID Not Found", message)
        {
        }
    }
}
