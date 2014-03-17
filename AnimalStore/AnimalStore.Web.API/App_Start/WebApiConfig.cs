using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using AnimalStore.Common.Constants;
using Newtonsoft.Json.Serialization;

namespace AnimalStore.Web.API
{
    public static class WebApiConfig
    {
        static HttpConfiguration _config;

        public static void Register(HttpConfiguration config)
        {
            _config = config;

            ConfigureMediaTypeMappings();

 //           CreateEventLogFileIfNotExists();

            _config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void CreateEventLogFileIfNotExists()
        {
            Common.Logging.EventLogHelper.InitialiseEventLog();
        }

        private static void ConfigureMediaTypeMappings()
        {
            _config.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping(
                    MediaTypeFormats.Keys.FORMATQUERYSTRINGKEY, MediaTypeFormats.Values.JSON.ToString(), "application/json"));

            _config.Formatters.XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping(
                    MediaTypeFormats.Keys.FORMATQUERYSTRINGKEY, MediaTypeFormats.Values.XML.ToString(), "application/xml"));

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
