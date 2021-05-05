using log4net;

namespace DaiPhatDat.Core.Kernel.Logger.Application
{
    /// <summary>
    /// tmquan
    /// Lớp đảm nhiệm ghi log
    /// </summary>
    public class LoggerServices : ILoggerServices
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void WriteInfo(string message)
        {
            log.Info(message);
        }

        public void WriteWarn(string message)
        {
            log.Warn(message);
        }

        public void WriteDebug(string message)
        {
            log.Debug(message);
        }

        public void WriteError(string message)
        {
            log.Error(message);
        }


    }
}