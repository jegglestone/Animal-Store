using AcceptanceTests.Utils;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class Whens : Steps
    {
        //private readonly IWebDriver _webDriver;

        public Whens()
        {
            //_webDriver = new FirefoxDriver();
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
    }
}
