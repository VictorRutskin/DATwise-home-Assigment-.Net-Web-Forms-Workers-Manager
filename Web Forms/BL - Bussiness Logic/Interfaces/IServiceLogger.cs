using System;

namespace BL.Interfaces
{
    public interface IServiceLogger
    {
        void InsertLog(string s);
        void InsertLog(Exception ex);
    }
}
