using System;
using AnimalStore.Common.Logging.Interfaces;
using log4net;
using log4net.Config;
using log4net.Layout;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using AnimalStore.Common.Constants;

namespace AnimalStore.Common.Logging
{
    public class LogManager : ILogManager
    {
        static LogManager()
        {
            var layout = new SimpleLayout();
            layout.ActivateOptions();

            var logAppender = new EventLogAppender();
            logAppender.Layout = layout;
            logAppender.LogName = EventLogConstants.EVENT_LOG_NAME;
            logAppender.ActivateOptions();

            var lossyAppender = new BufferingForwardingAppender();
            lossyAppender.Lossy = true;
            lossyAppender.BufferSize = 10;  // configurable
            lossyAppender.Evaluator = new log4net.Core.LevelEvaluator(log4net.Core.Level.Error);
            lossyAppender.AddAppender(logAppender);
            lossyAppender.ActivateOptions();

            Hierarchy h = (Hierarchy)log4net.LogManager.GetRepository();
            Logger root = h.Root;

            root.Level = log4net.Core.Level.All;

            log4net.Config.BasicConfigurator.Configure(lossyAppender);
        }

        public ILoggerWrapper GetLogger(Type type)
        {
            var logger = log4net.LogManager.GetLogger(type);
            return new LoggerAdapter(logger);
        }
    }
}
