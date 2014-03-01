using System.IO;
using System.Net;

namespace AnimalStore.Web.Helpers
{
    public class ResponseStreamHelper : IResponseStreamHelper
    {
        public Stream GetResponseStream(WebResponse response)
        {
            return response.GetResponseStream();
        }
    }
}