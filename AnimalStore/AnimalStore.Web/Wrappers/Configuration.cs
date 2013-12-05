using System.Configuration;
using AnimalStore.Web.Wrappers.Interfaces;
using AnimalStore.Web.ViewModels;

namespace AnimalStore.Web.Wrappers
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