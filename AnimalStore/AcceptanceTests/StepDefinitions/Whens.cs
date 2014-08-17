using AcceptanceTests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class Whens : Steps
    {
        private enum breeds
        {
            Affenpinscher = 1,
            Bulldog = 4,
            AustralianCattleDog = 18
        }

        private enum places
        {
            AbKettleby = 1,
            Leeds = 12472
        }

        [When(@"I select the '(.*)' option from the main menu")]
        [When(@"I click on a '(.*)'")]
        public void WhenIClickOnA(string linkText)
        {
            WebDriverAdapter.WebDriver.FindElement(By.PartialLinkText(linkText)).Click();
        }

        [When(@"I select the '(.*)' option from the sub menu")]
        public void WhenISelectTheOptionFromTheSubMenu(string linkText)
        {
            WebDriverAdapter.WebDriver.FindElement(By.PartialLinkText(linkText)).Click();
        }

        [When(@"I have selected any breed of dog in the UK")]
        public void WhenIveSelectedAnyBreedOfDogInTheUk()
        {
            SelectBreedByText("Breed (any)");

            SelectNationalSearchCheckBox();
        }

        [When(@"I have selected a (.*) anywhere in the UK")]
        public void WhenIveSelectedaDogAnywhereInTheUk(string breed)
        {
            SelectBreedByText(breed);

            SelectNationalSearchCheckBox();
        }

        [When(@"I have selected a (.*) within a (.*)")]
        public void WhenIHaveSelectedABulldogWithinALeeds(string breed, string location)
        {
            SelectBreedByText(breed);

            FillLocationTextBox(location);
        }

        [When(@"I press the search button")]
        public void WhenIPressTheSearchButton()
        {
            WebDriverAdapter.WebDriver.FindElement(By.Id("search")).Click();
        }

        [When(@"there is less than five matching (.*)")]
        public void WhenThereLessThanFiveMatchingDogs(string breed)
        {
            Assert.IsTrue(breed == "Bulldog");
        }

        [When(@"I make a GET request to the dogs API with the breedID")]
        public void WhenIMakeGETRequestToTheDogsAPIWithTheBreedId()
        {
            var resourceUri = NavigationHelper.GetAPIUrl("Dogs");
            resourceUri += "?breedid=" + (int)breeds.Bulldog + "&page=1&pagesize=100&format=json";

            sendASyncRequest(resourceUri);
        }

        [When(@"I make a GET request to the dogs API with a breedID and a (.*)")]
        public void WhenIMakeaGETRequestToTheDogsAPIWithABreedIdAndAPlaceId(int placeId)
        {
            var resourceUri = NavigationHelper.GetAPIUrl("Dogs");
            resourceUri += "?breedid=" + (int)breeds.Affenpinscher + "&page=1&pagesize=100&placeId=" + placeId + "&format=json";

            sendASyncRequest(resourceUri);
        }

        [When(@"there are no matching results in the API")]
        public void WhenThereAreNoMatchingResultsInTheAPI()
        {
            var resourceUri = NavigationHelper.GetAPIUrl("Dogs");
            resourceUri += "?breedid=" + (int)breeds.AustralianCattleDog + "&page=1&pagesize=100&placeId=" + (int)places.AbKettleby + "&format=json";

            sendASyncRequest(resourceUri);
        }

        [When(@"I make a GET request to the dogs API with a breedID and a placeId with a small pagesize")]
        public void WhenIMakeAGETRequestToTheDogsAPIWithABreedIDAndAPlaceIdWithASmallPagesize()
        {
            var resourceUri = NavigationHelper.GetAPIUrl("Dogs");
            resourceUri += "?breedid=" + (int)breeds.Bulldog + "&page=1&pagesize=5&placeid=" + (int)places.Leeds + "&format=json";
            sendASyncRequest(resourceUri);
        }

        private static void sendASyncRequest(string resourceUri)
        {
            var httpClient = ScenarioContext.Current.Get<HttpClient>();

            var responseMessage =
                httpClient
                    .GetAsync(resourceUri)
                    .Result;

            ScenarioContext.Current.Set(responseMessage);
        }

        private static void SelectNationalSearchCheckBox()
        {
            var checkBoxForNationalSearch = WebDriverAdapter.WebDriver.FindElement
                (By.CssSelector(
                    "#fragment-1 > form:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(3) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(3) > td:nth-child(1) > input:nth-child(1)"));
            checkBoxForNationalSearch.Click();
        }

        private static void SelectBreedByText(string text)
        {
            var dropdown = new SelectElement(WebDriverAdapter.WebDriver.FindElement(By.Id("SelectedBreed")));
            dropdown.SelectByText(text);
        }


        private void FillLocationTextBox(string location)
        {
            var locationTextBox = WebDriverAdapter.WebDriver.FindElement(By.CssSelector("#Location"));

            locationTextBox.SendKeys(location);
        }


        private static void Navigate(string url)
        {
            WebDriverAdapter.WebDriver.Url = url;
            WebDriverAdapter.WebDriver.Navigate(); 
        }
    }
}
