using DAL.DbContext;
using DAL.Interfaces;
using DAL.Models;
using System.Threading.Tasks;
using System;

public class LogManager : ILogManager
{
    private readonly myDbContext _context;

    public LogManager(myDbContext context)
    {
        _context = context;
    }

    public async Task LogExceptionAsync(Exception ex)
    {
            var logEntry = new Log
            {
                Time = DateTime.Now,
                ExceptionType = ex.GetType().Name,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                InnerExceptionMessage = ex.InnerException?.Message
            };

            _context.Logs.Add(logEntry);
            await _context.SaveChangesAsync();
    }
}
