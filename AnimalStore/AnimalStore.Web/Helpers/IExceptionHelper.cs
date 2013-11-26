using System;

namespace AnimalStore.Web.Helpers
{
    public interface IExceptionHelper
    {
        void HandleException(string exceptionMessage, Exception exception, Object classWhereExceptionOriginated);
    }
}