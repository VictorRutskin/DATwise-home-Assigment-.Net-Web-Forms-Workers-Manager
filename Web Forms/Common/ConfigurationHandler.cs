using System.Configuration;

namespace Common.ConfigurationHandler
{
    public static class ConfigurationHandler
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DATwiseDbConnection"].ConnectionString;

        public static string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
