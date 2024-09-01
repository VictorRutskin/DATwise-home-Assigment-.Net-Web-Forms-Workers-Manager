//using Microsoft.Extensions.Configuration;
//using DAL.Interfaces;
//using BL.Interfaces;

//namespace BL.Services
//{
//    public class ServiceLogger(IConfiguration config, ILogManager logManager) : IServiceLogger
//    {
//        private readonly IConfiguration _configuration = config;
//        private readonly ILogManager _logManager = logManager;

//        public void InsertLog(string s)
//        {
//            _logManager.InsertLog(s);
//        }
//        public void InsertLog(Exception ex)
//        {
//            _logManager.InsertLog(ex);
//        }
//    }
//}
