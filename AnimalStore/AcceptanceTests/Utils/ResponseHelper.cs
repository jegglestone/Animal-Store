using System.Net.Http;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Utils
{
    public static class ResponseHelper
    {
        public static T GetResponseAs<T>()
        {
            var responseMessage =
                ScenarioContext.Current.Get<HttpResponseMessage>();

            var content =
                responseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
