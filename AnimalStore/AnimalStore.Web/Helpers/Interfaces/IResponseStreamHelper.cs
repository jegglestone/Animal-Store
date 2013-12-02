using System.IO;
using System.Net;

namespace AnimalStore.Web.Helpers.Interfaces
{
    public interface IResponseStreamHelper
    {
        Stream GetResponseStream(WebResponse response);
    }
}