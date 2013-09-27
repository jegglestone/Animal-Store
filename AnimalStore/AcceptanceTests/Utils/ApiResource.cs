using System.Configuration;

namespace AcceptanceTests.Utils
{
    public class ApiResource
    {
        public static string GetResourceUrl(string pageName)
        {
            return GetSiteUrl() + pageName;
        }

        private static string GetSiteUrl()
        {
            return ConfigurationManager.AppSettings["SiteUrl"];
        }
    }
}
