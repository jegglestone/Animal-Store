﻿using AcceptanceTests.Utils;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class Givens : Steps
    {
        [Given(@"I am on the '(.*)' page")]
        public void GivenIAmOnTheFollowingPage(string pageName)
        {
            var pageUrl = NavigationHelper.GetPageUrl(pageName);

            WebDriverAdapter.WebDriver.Url = pageUrl;
            WebDriverAdapter.WebDriver.Navigate();
        }
    }
}
