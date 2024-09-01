using System.Configuration;

namespace Common
{
    public static class MyConfigurationManager
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DATwiseDbConnection"].ConnectionString;

        public static string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
