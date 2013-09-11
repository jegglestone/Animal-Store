using System;

namespace AnimalStore.Common.Logging.Interfaces
{
    public interface ILogManager
    {
        ILoggerWrapper GetLogger(Type type);
    }
}
