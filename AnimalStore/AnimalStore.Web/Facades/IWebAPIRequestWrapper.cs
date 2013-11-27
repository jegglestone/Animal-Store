using System.Net;

namespace AnimalStore.Web.Facades
{
    public interface IWebAPIRequestWrapper
    {
        WebResponse GetResponse(string url);
    }
}