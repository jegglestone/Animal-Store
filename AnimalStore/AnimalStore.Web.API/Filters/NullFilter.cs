using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace AnimalStore.Web.API.Filters
{
    /// <summary>
    /// Filter to check for null responses and handle them with a Http 404 response code
    /// </summary>
    public class NullFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var response = actionExecutedContext.Response;

            object responseValue;
            bool hasContent = response.TryGetContentValue(out responseValue);
            
            if (!hasContent)
                CreateHttp404NotFoundResponse();
        }

        private static void CreateHttp404NotFoundResponse()
        {
            var responseMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(string.Format("404 resource not found")),
                ReasonPhrase = "Resource Not Found",
                StatusCode = HttpStatusCode.NotFound
            };
            throw new HttpResponseException(responseMsg);
        }
    }
}