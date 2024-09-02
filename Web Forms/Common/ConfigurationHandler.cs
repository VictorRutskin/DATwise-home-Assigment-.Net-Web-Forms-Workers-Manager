using System.Configuration;

namespace Common.ConfigurationHandler
{
    public static class ConfigurationHandler
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DATwiseDbConnection"].ConnectionString;
        private static readonly string _logFilePath = ConfigurationManager.AppSettings["LogFilePath"];

        public static string GetConnectionString()
        {
            return _connectionString;
        }

        public static string GetLogFilePath()
        {
            return _logFilePath;
        }
    }
}
