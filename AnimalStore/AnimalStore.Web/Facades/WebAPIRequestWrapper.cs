using System;
using System.Net;
using AnimalStore.Web.Helpers;

namespace AnimalStore.Web.Facades
{
    public class WebAPIRequestWrapper : IWebAPIRequestWrapper
    {
        private readonly IExceptionHelper _exceptionHelper;

        public WebAPIRequestWrapper(IExceptionHelper exceptionHelper)
        {
            _exceptionHelper = exceptionHelper;
        }

        public virtual WebResponse GetResponse(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Accept = "application/json";

                return request.GetResponse();
            }
            catch (Exception e)
            {
                _exceptionHelper.HandleException("Web Service not available", e, (GetType()));
            }
            return null;
        }
    }
}