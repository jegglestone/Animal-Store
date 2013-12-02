using System.IO;
using System.Net;
using AnimalStore.Web.Helpers.Interfaces;

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