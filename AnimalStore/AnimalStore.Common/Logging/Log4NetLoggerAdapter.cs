using AnimalStore.Common.Logging.Interfaces;
using log4net;

namespace AnimalStore.Common.Logging
{

  /// <summary>
  /// Adapter pattern - to create an ILog instance and wrap it over
  /// the Log4Net ILog object
  /// </summary>
  public class Log4NetLoggerAdapter : ILoggerWrapper
  {
    private readonly ILog _log;

    internal Log4NetLoggerAdapter(ILog log)
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

    public void Error(object message, System.Exception exception)
    {
      _log.Error(message, exception);
    }

    public void Error(object message)
    {
      _log.Error(message);
    }

    public void Fatal(object message, System.Exception exception)
    {
      _log.Fatal(message, exception);
    }

    public void Info(object message, System.Exception exception)
    {
      _log.Info(message, exception);
    }

    public void Info(object message)
    {
      _log.Info(message);
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

    public log4net.Core.ILogger Logger
    {
      get { return _log.Logger; }
    }
  }
}
