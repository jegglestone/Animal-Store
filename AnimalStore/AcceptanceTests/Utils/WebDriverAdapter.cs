namespace AcceptanceTests.Utils
{
  using OpenQA.Selenium.PhantomJS;
  using System;
  using OpenQA.Selenium;

  public static class WebDriverAdapter
  {
    private static readonly IWebDriver _webDriver;

    public static IWebDriver WebDriver
    {
      get { return _webDriver; }
    }

    static WebDriverAdapter()
    {
      _webDriver = new PhantomJSDriver();
      _webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
    }

    public static void StartAPI()
    {
      _webDriver.Url = NavigationHelper.GetApiBaseUrl();
      _webDriver.Navigate();
    }

    public static void Dispose()
    {
      _webDriver.Close();
      _webDriver.Dispose();
    }
  }
}
