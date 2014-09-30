using AcceptanceTests.Utils;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Hooks
{
  [Binding]
  public class AfterTestRunHook
  {
    [AfterTestRun]
    public static void TearDownWebDriver()
    {
      WebDriverAdapter.Dispose();
    }
  }
}
