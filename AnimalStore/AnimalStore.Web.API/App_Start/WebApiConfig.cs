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

            MakeJsonCamelCase();

            SetDefaultMediaTypeToJson();

        }

        private static void MakeJsonCamelCase()
        {
            var jsonFormatter = _config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
        }

        private static void SetDefaultMediaTypeToJson()
        {
            _config.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                new MediaTypeHeaderValue("text/html"));
        }
    }
}
