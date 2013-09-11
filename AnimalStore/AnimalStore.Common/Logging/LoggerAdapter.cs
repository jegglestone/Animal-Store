﻿using AnimalStore.Common.Logging.Interfaces;
using log4net;

namespace AnimalStore.Common.Logging
{

    /// <summary>
    /// Adapter pattern - to create an ILog instance and wrap it over
    /// the Log4Net ILog object
    /// </summary>
    public class LoggerAdapter : ILoggerWrapper
    {
        private readonly ILog _log;

        internal LoggerAdapter(ILog log)
        {
            _log = log;
        }

        public void Debug(object message, System.Exception exception)
        {
            _log.Debug(message, exception);
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }

        public void DebugFormat(System.IFormatProvider provider, string format, params object[] args)
        {
            _log.DebugFormat(provider, format, args);
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            _log.DebugFormat(format, arg0, arg1, arg2);
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            _log.DebugFormat(format, arg0, arg1);
        }

        public void DebugFormat(string format, object arg0)
        {
            _log.DebugFormat(format, arg0);
        }

        public void DebugFormat(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        public void Error(object message, System.Exception exception)
        {
            _log.Error(message, exception);
        }

        public void Error(object message)
        {
            _log.Error(message);
        }

        public void ErrorFormat(System.IFormatProvider provider, string format, params object[] args)
        {
            _log.ErrorFormat(provider, format, args);
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            _log.ErrorFormat(format, arg0, arg1, arg2);
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            _log.ErrorFormat(format, arg0, arg1);
        }

        public void ErrorFormat(string format, object arg0)
        {
            _log.ErrorFormat(format, arg0);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }

        public void Fatal(object message, System.Exception exception)
        {
            _log.Fatal(message, exception);
        }

        public void Fatal(object message)
        {
            _log.Fatal(message);
        }

        public void FatalFormat(System.IFormatProvider provider, string format, params object[] args)
        {
            _log.FatalFormat(provider, format, args);
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            _log.FatalFormat(format, arg0, arg1, arg2);
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            _log.FatalFormat(format, arg0, arg1);
        }

        public void FatalFormat(string format, object arg0)
        {
            _log.FatalFormat(format, arg0);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _log.FatalFormat(format, args);
        }

        public void Info(object message, System.Exception exception)
        {
            _log.Info(message, exception);
        }

        public void Info(object message)
        {
            _log.Info(message);
        }

        public void InfoFormat(System.IFormatProvider provider, string format, params object[] args)
        {
            _log.InfoFormat(provider, format, args);
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            _log.InfoFormat(format, arg0, arg1, arg2);
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            _log.InfoFormat(format, arg0, arg1);
        }

        public void InfoFormat(string format, object arg0)
        {
            _log.InfoFormat(format, arg0);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        public bool IsDebugEnabled
        {
            get { return _log.IsDebugEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return _log.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _log.IsFatalEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return _log.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _log.IsWarnEnabled; }
        }

        public void Warn(object message, System.Exception exception)
        {
            _log.Warn(message, exception);
        }

        public void Warn(object message)
        {
            _log.Warn(message);
        }

        public void WarnFormat(System.IFormatProvider provider, string format, params object[] args)
        {
            _log.WarnFormat(provider, format, args);
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            _log.WarnFormat(format, arg0, arg1, arg2);
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            _log.WarnFormat(format, arg0, arg1);
        }

        public void WarnFormat(string format, object arg0)
        {
            _log.WarnFormat(format, arg0);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }

        public log4net.Core.ILogger Logger
        {
            get { return _log.Logger; }
        }
    }
}
