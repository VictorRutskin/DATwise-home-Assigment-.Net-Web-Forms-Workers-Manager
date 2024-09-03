using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ILogManager
    {
        Task LogExceptionAsync(Exception ex);

    }
}
