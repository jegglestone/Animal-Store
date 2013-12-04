using AcceptanceTests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
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

        [Then(@"I should be presented with a list of dog search results")]
        public void ThenIShouldBePresentedWithAListOfDogSearchResults()
        {
            var resultsTableBody = GetResultsTable();
           
            Assert.That(resultsTableBody.Text.Contains("Name:"));
            Assert.That(resultsTableBody.Text.Contains("Age:"));
            Assert.That(resultsTableBody.Text.Contains("Affen Pinscher."));
        }

        [Then(@"I should be presented with search results for the (.*)")]
        public void ThenIShouldBePresentedWithSearchResultsForTheBreed(string breed)
        {
            var resultsTableBody = GetResultsTable();

            Assert.That(resultsTableBody.Text.Contains("Name:"));
            Assert.That(resultsTableBody.Text.Contains("Age:"));
            Assert.That(resultsTableBody.Text.Contains(breed));
        }

        [Then(@"some other dogs in the (.*)")]
        public void ThenSomeOtherDogsInTheSameCategory(string breed)
        {
            ThenIShouldBePresentedWithSearchResultsForTheBreed(breed);
        }


        private static IWebElement GetResultsTable()
        {
            return WebDriverAdapter.WebDriver.FindElement(By.XPath("/html/body/div/section/table"));
        }
    }
}
