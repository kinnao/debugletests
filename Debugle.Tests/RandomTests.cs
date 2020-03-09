using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Debugle.Tests
{
    public class RandomTests
    {
        private const string _correctLogin = "deveducation1@i.ua";
        private const string _correctPassword = "Qwerty123";
        private const string _loginPageUrl = "https://debugle.com/login";

        [Fact]
        public void AddAndRemoveIssue()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_loginPageUrl);
            LoginPage loginPage = new LoginPage(driver);
            loginPage.TypeUserLogin(_correctLogin);
            loginPage.TypeUserPassword(_correctPassword);
            loginPage.ClickLoginButton();

            ProjectsListPage projectsListPage = new ProjectsListPage(driver);
            driver.FindElement(By.CssSelector("a[title='ListBoxer']")).Click();

            driver.FindElement(By.Id("issue-name")).SendKeys("Unique issues name 78999");
            driver.FindElement(By.XPath("//button[text()='Add']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            IWebElement addedIssue = wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[text()='Unique issues name 78999']")));
            addedIssue.Click();
            Thread.Sleep(3000);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            IWebElement deleteIcon = wait.Until(ExpectedConditions.ElementExists(By.XPath("//ul[@class='right']/li[@class='delete']/i[@class='icon-trash']")));
            deleteIcon.Click();

        }
    }
}