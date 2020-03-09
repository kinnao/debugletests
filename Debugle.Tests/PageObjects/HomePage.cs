using OpenQA.Selenium;
using System;

namespace Debugle.Tests
{
    public class HomePage
    {
        private IWebDriver _driver;
        By _loginLinkLocator = By.LinkText("LOGIN");

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            if (_driver.FindElements(By.XPath("//h2[text()='Track anything, collaborate and stay organized. Debugle is the best choice for freelancers and small teams.']")).Count  == 0)
            {
                throw new InvalidOperationException("It is not Home page");
            }
        }

        public void ClickLoginLink()
        {
            _driver.FindElement(_loginLinkLocator).Click();
        }
    }
}
