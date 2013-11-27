using System.Net;

namespace AnimalStore.Web.Facades
{
    public class WebAPIRequestWrapper : IWebAPIRequestWrapper
    {
        public virtual WebResponse GetResponse(string url)
        {
            var request = WebRequest.Create(url);
            return request.GetResponse();
        }
    }
}