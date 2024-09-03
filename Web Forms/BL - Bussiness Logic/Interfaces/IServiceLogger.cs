using System;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IServiceLogger
    {
        Task LogErrorAsync(Exception ex);
    }
}
