using System.Net;

namespace AnimalStore.Web.Facades.Interfaces
{
    public interface IWebAPIRequestWrapper
    {
        WebResponse GetResponse(string url);
    }
}