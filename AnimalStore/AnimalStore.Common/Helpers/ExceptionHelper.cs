using System;
using AnimalStore.Common.Logging;

namespace AnimalStore.Common.Helpers
{
    public class ExceptionHelper : IExceptionHelper
    {
        private readonly LogManager _logManager;

        public ExceptionHelper(LogManager logManager)
        {
            _logManager = logManager;
        }

        public void HandleException(string exceptionMessage, Exception exception, Object classWhereExceptionOriginated)
        {
            var log = _logManager.GetLogger(classWhereExceptionOriginated.GetType());
            log.Error(exceptionMessage, exception);
        }
    }
}