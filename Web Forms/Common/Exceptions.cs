using System;

namespace Common.CustomExceptions
{
    public class CustomException : Exception
    {
        public string Name { get; }
        public string Description { get; }
        public string Explanation { get; }
        public DateTime Time { get; }

        public CustomException(string name, string description, string explanation)
            : base($"{name}: {description}")
        {
            Name = name;
            Description = description;
            Explanation = explanation;
            Time = DateTime.Now;
        }
    }

    public class DatabaseLoggingException : CustomException
    {
        public DatabaseLoggingException(string message, Exception innerException)
            : base("DatabaseLoggingException", message, innerException.Message)
        {
        }
    }

    public class FileLoggingException : CustomException
    {
        public FileLoggingException(string message, Exception innerException)
            : base("FileLoggingException", message, innerException.Message)
        {
        }
    }

    public class DatabaseAccessException : CustomException
    {
        public DatabaseAccessException(string message, Exception innerException)
            : base("DatabaseAccessException", message, innerException.Message)
        {
        }
    }

    public class ValidationException : CustomException
    {
        public ValidationException(string message, Exception innerException)
            : base("ValidationException", message, innerException.Message)
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
