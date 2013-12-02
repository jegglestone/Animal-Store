using System;

namespace AnimalStore.Common.Helpers
{
    public interface IExceptionHelper
    {
        void HandleException(string exceptionMessage, Exception exception, Object classWhereExceptionOriginated);
    }
}