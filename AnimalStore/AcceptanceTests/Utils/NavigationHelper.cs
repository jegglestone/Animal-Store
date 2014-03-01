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

        public static string GetAPIUrl(string resourceName)
        {
            var resource = resourceName.Replace(" ", String.Empty).ToLower();

            if (!_resourceMap.ContainsKey(resource))
                throw new KeyNotFoundException(String.Format("A resource for {0}.", resourceName));

            return GetApiBaseUrl() + _resourceMap[resource];
        }

        private static string GetPageFromName(string pageName)
        {
            var page = pageName.Replace(" ", String.Empty).ToLower();

            if (!_pageMap.ContainsKey(page))
                throw new KeyNotFoundException(String.Format("A page for {0}.", pageName));

            return GetSiteBaseUrl() + _pageMap[page];
        }

        private static readonly IDictionary<string, string> _pageMap =
            new Dictionary<string, string>
                {
                    {PageNames.HOME, PageUrLs.HOME},
                    {PageNames.ABOUT, PageUrLs.ABOUT},
                    {PageNames.CONTACT, PageUrLs.CONTACT}
                };


        private static readonly IDictionary<string, string> _resourceMap =
            new Dictionary<string, string>
                {
                    {ResourceNames.DOGS, ResourceUrLs.DOGS},
                };

        private static class PageNames
        {
            public const string HOME = "home";
            public const string ABOUT = "about";
            public const string CONTACT = "contact";
        }

        private static class PageUrLs
        {
            public const string HOME = "/";
            public const string ABOUT = "/home/about";
            public const string CONTACT = "/home/contact";
        }

        private static class ResourceNames
        {
            public const string DOGS = "dogs";
        }

        private static class ResourceUrLs
        {
            public const string DOGS = "/dogs";
        }

        private static string GetSiteBaseUrl()
        {
            return ConfigurationManager.AppSettings["SiteUrl"];
        }

        public static string GetApiBaseUrl()
        {
            return ConfigurationManager.AppSettings["APIUrl"];
        }
    }
}
