using AcceptanceTests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class Whens : Steps
    {
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
            var resourceUrl = NavigationHelper.GetAPIUrl("Dogs");
            resourceUrl += "?breedid=1&page=1&pagesize=100&format=json"; //TODO: constant
            Navigate(resourceUrl);
        }

        [When(@"I make a GET request to the dogs API with a breedID and a placeId")]
        public void WhenIMakeaGETRequestToTheDogsAPIWithABreedIdAndAPlaceId()
        {
            var resourceUrl = NavigationHelper.GetAPIUrl("Dogs");
            resourceUrl += "?breedid=1&page=1&pagesize=100&placeId=1&format=json"; //TODO: constant
            Navigate(resourceUrl);
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

        private static void Navigate(string url)
        {
            WebDriverAdapter.WebDriver.Url = url;
            WebDriverAdapter.WebDriver.Navigate(); 
        }
    }
}
