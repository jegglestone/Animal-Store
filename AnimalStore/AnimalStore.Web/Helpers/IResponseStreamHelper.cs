using System.IO;
using System.Net;

namespace AnimalStore.Web.Helpers
{
    public interface IResponseStreamHelper
    {
        Stream GetResponseStream(WebResponse response);
    }
}