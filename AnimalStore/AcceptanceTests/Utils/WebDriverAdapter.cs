using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace AcceptanceTests.Utils
{
    public static class WebDriverAdapter
    {
        private static readonly IWebDriver _webDriver;

        public static IWebDriver WebDriver
        {
            get { return _webDriver; }
        }

        static WebDriverAdapter()
        {
            var profile = new FirefoxProfile { EnableNativeEvents = true };

            _webDriver = new FirefoxDriver(profile);
            _webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }

        public static void Dispose()
        {
            _webDriver.Close();
            _webDriver.Dispose();
        }
    }
}
