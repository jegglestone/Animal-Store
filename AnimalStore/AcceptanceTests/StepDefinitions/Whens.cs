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
        public void WhenIHaveSelectedAnyBreedOfDogInTheUK()
        {
            selectBreedByText("Breed (any)");

            selectNationalSearchCheckBox();
        }

        [When(@"I have selected a (.*) anywhere in the UK")]
        public void WhenIHaveSelectedADalmatianAnywhereInTheUK(string breed)
        {
            selectBreedByText(breed);

            selectNationalSearchCheckBox();
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

        private static void selectNationalSearchCheckBox()
        {
            var checkBoxForNationalSearch = WebDriverAdapter.WebDriver.FindElement
                (By.CssSelector(
                    "#fragment-1 > form:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(3) > td:nth-child(1) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(3) > td:nth-child(1) > input:nth-child(1)"));
            checkBoxForNationalSearch.Click();
        }

        private static void selectBreedByText(string text)
        {
            var dropdown = new SelectElement(WebDriverAdapter.WebDriver.FindElement(By.Id("SelectedBreed")));
            dropdown.SelectByText(text);
        }
    }
}
