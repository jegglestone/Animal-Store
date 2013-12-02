using System.Configuration;
using AnimalStore.Web.Facades.Interfaces;
using AnimalStore.Web.ViewModels;

namespace AnimalStore.Web.Facades
{
    public class Configuration : IConfiguration
    {
        private static string WebAPIUrl
        {
            get { return ConfigurationManager.AppSettings[AppSettingKeys.WebAPIUrl]; }
        }

        public string GetWebAPIUrl()
        {
            return WebAPIUrl;
        }
    }
}