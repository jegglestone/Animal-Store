using System;
using System.Collections.Generic;
using System.Configuration;

namespace AcceptanceTests.Utils
{
    public static class NavigationHelper
    {
        public static string GetPageUrl(string pageName)
        {
            return GetPageFromName(pageName);
        }

        private static string GetPageFromName(string pageName)
        {
            var page = pageName.Replace(" ", String.Empty).ToLower();

            if (!PageMap.ContainsKey(page))
                throw new KeyNotFoundException(String.Format("A page for {0}.", pageName));

            return GetSiteBaseUrl() + PageMap[page];
        }

        private static readonly IDictionary<string, string> PageMap =
            new Dictionary<string, string>
                {
                    {PageNames.Home, PageURLs.Home},
                    {PageNames.About, PageURLs.About},
                    {PageNames.Contact, PageURLs.Contact}
                };

        private static class PageNames
        {
            public const string Home = "home";
            public const string About = "about";
            public const string Contact = "contact";
        }

        private static class PageURLs
        {
            public const string Home = "/";
            public const string About = "/home/about";
            public const string Contact = "/home/contact";
        }

        private static string GetSiteBaseUrl()
        {
            return ConfigurationManager.AppSettings["SiteUrl"];
        }

        private static string GetApiBaseUrl()
        {
            return ConfigurationManager.AppSettings["APIUrl"];
        }
    }
}
