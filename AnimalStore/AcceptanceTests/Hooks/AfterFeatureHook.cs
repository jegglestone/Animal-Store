using AcceptanceTests.Utils;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Hooks
{
    [Binding]
    public class AfterFeatureHook
    {
        [AfterFeature()]
        public static void TearDownFeature()
        {
            WebDriverAdapter.Dispose();
        }
    }
}
