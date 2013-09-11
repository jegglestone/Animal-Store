using System.Web.Http;
using System.Net.Http.Formatting;
using System.Linq;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using AnimalStore.Common;

namespace AnimalStore.Web.API.App_Start
{
    public static class WebApiConfig
    {
        static HttpConfiguration _config;

        public static void Register(HttpConfiguration config)
        {
            _config = config;

            configureMediaTypeMappings();

            _config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void configureMediaTypeMappings()
        {
            // Json and Xml support
            _config.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping(
                    MediaTypeFormatConstants.Keys.FORMATQUERYSTRINGKEY, MediaTypeFormatConstants.Values.JSON, "application/json"));

            _config.Formatters.XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping(
                    MediaTypeFormatConstants.Keys.FORMATQUERYSTRINGKEY, MediaTypeFormatConstants.Values.XML, "application/xml"));

            // Make json Camel Case
            var jsonFormatter = _config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            // Make json default media type within the browser
            _config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
