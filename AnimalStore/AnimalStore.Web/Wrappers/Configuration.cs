using System.Configuration;
using AnimalStore.Web.Wrappers.Interfaces;
using AnimalStore.Web.ViewModels;

namespace AnimalStore.Web.Wrappers
{
    public class Configuration : IConfiguration
    {
        private static string webAPIUrl
        {
            get { return ConfigurationManager.AppSettings[AppSettingKeys.WebAPIUrl]; }
        }

        private static int defaultSearchResultPageSize
        {
            get { return int.Parse(ConfigurationManager.AppSettings[AppSettingKeys.DefaultSearchResultPageSize]); }
        }

        public string GetWebAPIUrl()
        {
            return webAPIUrl;
        }

        public int GetDefaultSearchResultPageSize()
        {
            return defaultSearchResultPageSize;
        }
    }
}