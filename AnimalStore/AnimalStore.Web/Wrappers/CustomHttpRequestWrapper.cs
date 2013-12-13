using System.Web;
using AnimalStore.Web.Wrappers.Interfaces;

namespace AnimalStore.Web.Wrappers
{
    public class CustomHttpRequestWrapper : ICustomHttpRequestWrapper
    {
        private readonly HttpRequest _httpRequest;

        public CustomHttpRequestWrapper(HttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public string GetQueryStringValueByKey(string key)
        {
            return _httpRequest.QueryString[key];
        }
    }
}