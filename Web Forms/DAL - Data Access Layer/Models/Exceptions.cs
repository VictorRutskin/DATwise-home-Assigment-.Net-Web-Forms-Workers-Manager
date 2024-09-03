using System;

namespace Dal.Models
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
        public DatabaseAccessException(string message)
            : base("DatabaseAccessException", "Access to the database failed.", message)
        {
        }
    }

    public class ValidationException : CustomException
    {
        public ValidationException(string message)
            : base("ValidationException", "Validation failed.", message)
        {
        }
    }

    public class EmployeeIDNotFoundInDbException : CustomException
    {
        public EmployeeIDNotFoundInDbException(string message)
            : base("EmployeeIDNotFoundInDbException", "EmployeeID Not Found", message)
        {
        }
    }
}
