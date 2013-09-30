using AcceptanceTests.Utils;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class Thens : Steps
    {
        [Then(@"I am taken to the '(.*)' page")]
        public void ThenIAmTakenToThePage(string pageName)
        {
            var actualPageUrl = WebDriverAdapter.WebDriver.Url;
            var expectedPageUrl = NavigationHelper.GetPageUrl(pageName);

            StringAssert.AreEqualIgnoringCase(
                expectedPageUrl, 
                actualPageUrl, 
                string.Format("Current Page URI: {0}", actualPageUrl));
        }
    }
}
