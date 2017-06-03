using System;
using System.IO;
using System.Reflection;
using System.Xml;
using log4net;
using log4net.Config;

namespace ArabicTutorials.Common.Logger
{
    public class Logger : ILogger
    {
        private static readonly ILog I4NetLogger = LogManager.GetLogger(typeof(Logger));

        static Logger()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            var repo = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
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
