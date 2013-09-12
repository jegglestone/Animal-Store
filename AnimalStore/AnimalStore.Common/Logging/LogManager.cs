using System;
using AnimalStore.Common.Logging.Interfaces;
using log4net.Config;

namespace AnimalStore.Common.Logging
{
    public class LogManager : ILogManager
    {
        static LogManager()
        {
            XmlConfigurator.Configure(
                new System.IO.FileInfo("log4net.config"));
        }

        public ILoggerWrapper GetLogger(Type type)
        {
            var logger = log4net.LogManager.GetLogger(type);
            return new LoggerAdapter(logger);
        }
    }
}
