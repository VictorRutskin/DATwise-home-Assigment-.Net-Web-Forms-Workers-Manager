using System;

namespace DAL.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string InnerExceptionMessage { get; set; }
    }
}
