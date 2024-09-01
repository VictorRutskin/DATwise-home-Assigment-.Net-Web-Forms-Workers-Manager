using System;

namespace DAL.Interfaces
{
    public interface ILogManager
    {
        void InsertLog(Exception exception);
        void InsertLog(string message);
    }
}
