using System;
using AnimalStore.Common.Logging.Interfaces;

namespace AnimalStore.Common.Logging
{
  public class AzureLoggerAdapter :ILoggerWrapper
  {
    public void Debug(object message)
    {
      
    }

    public void Debug(object message, Exception exception)
    {

    }

    public void Error(object message)
    {

    }

    public void Error(object message, Exception exception)
    {
  
    }

    public void Fatal(object message, Exception exception)
    {

    }

    public void Info(object message)
    {

    }

    public void Info(object message, Exception exception)
    {

    }

    public void Warn(object message)
    {

    }

    public void Warn(object message, Exception exception)
    {

    }

    public bool IsInfoEnabled
    {
      get { return true; }
    }
  }
}
