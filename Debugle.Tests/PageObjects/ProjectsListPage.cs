using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Debugle.Tests
{
    public class ProjectsListPage
    {
        private IWebDriver _driver;
        By _createProjectButton = By.ClassName("create-project");
        By _upgradeSubscriptionParagraph = By.ClassName("upgrade");

        public ProjectsListPage(IWebDriver driver)
        {
            _driver = driver;
            if (_driver.FindElements(By.XPath("//h1[text()='Projects']")).Count == 0)
            {
                throw new InvalidOperationException("It is not Projects List page");
            }
        }

        public void ClickCreateProjectButton()
        {
            _driver.FindElement(_createProjectButton).Click();
        }

        public string GiveUpgradeSubsriptionText()
        {
            return _driver.FindElement(_upgradeSubscriptionParagraph).Text;
        }
    }
}
