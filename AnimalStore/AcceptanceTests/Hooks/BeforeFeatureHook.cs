using AcceptanceTests.Utils;
using TechTalk.SpecFlow;
namespace AcceptanceTests.Hooks
{
    [Binding]
    public class BeforeFeatureHook
    {
        [BeforeFeature("SearchWidget")]
        public static void StartApi()
        {
            WebDriverAdapter.StartAPI();
        }
    }
}
