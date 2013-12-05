using System.Net;

namespace AnimalStore.Web.Wrappers.Interfaces
{
    public interface IWebAPIRequestWrapper
    {
        WebResponse GetResponse(string url);
    }
}