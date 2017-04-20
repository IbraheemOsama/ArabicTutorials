using System;
using log4net;

namespace ArabicTutorials.Common.Logger
{
    public class Logger : ILogger
    {
        private static readonly ILog I4NetLogger = LogManager.GetLogger("ArabicTutorialsLogger");

        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public void LogError(Exception ex)
        {
            I4NetLogger.Error(ex);
        }

        public void LogError(string exMessage)
        {
            I4NetLogger.Error(exMessage);
        }

        public void LogInfo(string message)
        {
            I4NetLogger.InfoFormat(message);
        }
    }
}
