namespace AcceptanceTests.Hooks
{
  using System.Net.Http;
  using TechTalk.SpecFlow;

  [Binding]
  public class Hooks
  {
    [BeforeScenario]
    public static void BeforeScenario()
    {
      ScenarioContext.Current.Set(new HttpClient());
    }

    [AfterScenario]
    public static void AfterScenario()
    {
      var httpClient =
        ScenarioContext.Current.Get<HttpClient>();

      httpClient.Dispose();
    }
  }
}