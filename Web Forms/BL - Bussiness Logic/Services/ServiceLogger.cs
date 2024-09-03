using BL.Interfaces;
using DAL.DbContext;
using DAL.Interfaces;
using System.IO;
using System.Threading.Tasks;
using System;
using Common.CustomExceptions;

public class ServiceLogger : IServiceLogger
{
    private readonly ILogManager _logManager;
    private readonly string _logFilePath;

    public ServiceLogger(myDbContext dbContext, string logFilePath)
    {
        _logFilePath = logFilePath;
        _logManager = new LogManager(dbContext);
    }

    public async Task LogErrorAsync(Exception ex)
    {
        try
        {
            await _logManager.LogExceptionAsync(ex);
        }
        catch (Exception dbEx)
        {
            LogToFile(ex, dbEx);
        }
    }

    private void LogToFile(Exception originalEx, Exception dbEx)
    {
        try
        {
            using (var writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine("--------------------------------------------------");
                writer.WriteLine($"Time: {DateTime.Now}");
                writer.WriteLine($"Original Exception: {originalEx.GetType().Name}");
                writer.WriteLine($"Message: {originalEx.Message}");
                writer.WriteLine($"StackTrace: {originalEx.StackTrace}");
                if (originalEx.InnerException != null)
                {
                    writer.WriteLine($"Inner Exception: {originalEx.InnerException.Message}");
                }
                writer.WriteLine($"Database Logging Exception: {dbEx.Message}");
                writer.WriteLine("--------------------------------------------------");
                writer.WriteLine();
            }
        }
        catch (Exception fileEx)
        {
            throw new FileLoggingException("Failed to log exception to file.", fileEx);
        }
    }
}
