using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
