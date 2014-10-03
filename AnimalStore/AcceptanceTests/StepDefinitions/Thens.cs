using System.Linq;
using AcceptanceTests.Utils;
using AnimalStore.Model;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using System.Net.Http;

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

        [Then(@"I should be presented with search results for that (.*) within the area of (.*)")]
        public void ThenIShouldBePresentedWithSearchResultsForThatBulldogWithinKmOfArea(string breed, string location)
        {
            var resultsTableBody = GetResultsTable();

            Assert.That(resultsTableBody.Text.Contains("Name:"));
            Assert.That(resultsTableBody.Text.Contains("Age:"));
            Assert.That(resultsTableBody.Text.Contains(breed));
            Assert.That(resultsTableBody.Text.Contains(location));
        }

      [Then(@"I should be presented with search results for that (.*) within the postcode of (.*)")]
      public void ThenIShouldBePresentedWithSearchResultsForThatBulldogWithinPostocde(string breed, string location)
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

        [Then(@"I should be presented with a JSON response relevant to the breed and filtered by (.*)")]
        public void ThenIShouldBePresentedWithJSONResultsRelevantToTheBreedAndFilteredByPlace(int placeId)
        {
            var response = ResponseHelper.GetResponseAs<PageableResults<Dog>>();

            Assert.That(response.Data.First().Breed.Name == "Affenpinscher");
            Assert.That(response.Data.First().Breed.Category.Name == "Toy");
            Assert.That(response.Data.First().PlaceId == placeId);
        }

        [Then(@"I should be presented with JSON results relevant to the breed")]
        public void ThenIShouldBePresentedWithJSONResultsRelevantToTheBreed()
        {
            var response = ResponseHelper.GetResponseAs<PageableResults<Dog>>();

            Assert.That(response.Data.First().Breed.Name == "Bulldog");
            Assert.That(response.Data.First().Breed.Category.Name == "Non-sporting");
        }

        [Then(@"there should be other dogs in the same category in the results")]
        public void ThenThereShouldBeOtherDogsInTheSameCategoryInTheResults()
        {
            var response = ResponseHelper.GetResponseAs<PageableResults<Dog>>();

            Assert.That(response.Data.Any(x => x.Breed.Name == "Dalmatian"));
        }

        [When(@"I should be able to navigate the results through using the paging links")]
        public void WhenIShouldBeAbleToNavigateTheResultsThroughUsingThePagingLinks()
        {
            var response = ResponseHelper.GetResponseAs<PageableResults<Dog>>();

            Assert.That(response.Data.Count() == 5);
        }

        [Then(@"the response is a status code (.*)")]
        public void ThenTheResponseIsAStatusCode(int expectedStatusCode)
        {
            var responseMessage =
                ScenarioContext.Current.Get<HttpResponseMessage>();

            var actualStatusCode = (int)responseMessage.StatusCode;

            Assert.That(actualStatusCode, Is.EqualTo(expectedStatusCode));
        }

        [Then(@"the search description should say '(.*)'")]
        public void ThenTheSearchDescriptionShouldSay(string expectedSearchDescription)
        {
            var actualResultDescription = WebDriverAdapter.WebDriver.FindElement(By.CssSelector("section.content-wrapper > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(1)"));

            Assert.That(actualResultDescription.Text, Is.EqualTo(expectedSearchDescription));
        }


        private static IWebElement GetResultsTable()
        {
            return WebDriverAdapter.WebDriver.FindElement(By.XPath("/html/body/div/section/table"));
        }
    }
}
