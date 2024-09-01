//using DAL.Interfaces;
//using DAL.Contexts;
//using DAL.Models;

//namespace DAL.Managers
//{
//    public class LogManager : ILogManager
//    {
//        private readonly Context _context;
//        public LogManager(Context context)
//        {
//            _context = context;
//        }

//        public void InsertLog(Exception exception)
//        {
//            try
//            {
//                Log l = new(exception);
//                _context.Logs.AddAsync(l);
//                _context.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                WriteToLogFile(ex.ToString());
//            }
//        }

//        public void InsertLog(string message)
//        {

//            try
//            {
//                Log l = new(message);
//                _context.Logs.AddAsync(l);
//                _context.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                WriteToLogFile(ex.ToString());
//            }
//        }

//        private void WriteToLogFile(string msg)
//        {
//            string currentDateDirectory = DateTime.Now.ToString("ddMMyy");
//            string path = currentDateDirectory;
//            string fileName = "errorLog.txt";

//            string pathWithFileName = path + "\\" + fileName;

//            if (!Directory.Exists(path))
//            {
//                Directory.CreateDirectory(path);
//            }
//            using (StreamWriter sw = File.AppendText(pathWithFileName))
//            {
//                sw.WriteLine(msg + ";" + Environment.NewLine);
//            }
//        }
//    }
//}