using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Debugle.Tests
{
    public class ProjectsListPageTests
    {
        private const string _correctLogin = "deveducation1@i.ua";
        private const string _correctPassword = "Qwerty123";
        private const string _loginPageUrl = "https://debugle.com/login";

        [Fact]
        public void CheckThatItIsPossibleToOpenProjectsListPageAfterSuccessfulLogin()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_loginPageUrl);
            LoginPage loginPage = new LoginPage(driver);
            loginPage.TypeUserLogin(_correctLogin);
            loginPage.TypeUserPassword(_correctPassword);
            loginPage.ClickLoginButton();

            try
            {
                ProjectsListPage projectsListPage = new ProjectsListPage(driver);
            }
            catch
            {
                Assert.True(false, "ProjectsList page wasn't opened.");
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        public void CheckThatItIsNotPossibleToCreateMoreThan1Project()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_loginPageUrl);
            LoginPage loginPage = new LoginPage(driver);
            loginPage.TypeUserLogin(_correctLogin);
            loginPage.TypeUserPassword(_correctPassword);
            loginPage.ClickLoginButton();
            ProjectsListPage projectsListPage = new ProjectsListPage(driver);

            projectsListPage.ClickCreateProjectButton();
            string actual = projectsListPage.GiveUpgradeSubsriptionText();

            driver.Quit();

            Assert.Contains("Your current subscription doesn't allow you to own more than 1 project.", actual);
        }
    }
}
