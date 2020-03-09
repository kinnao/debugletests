using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Debugle.Tests
{
    public class HomePageTests
    {
        private const string _homePageUrl = "https://debugle.com";

        [Fact]
        public void CheckThatItIsPossibleToOpenLoginPageByClickingLoginLink()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_homePageUrl);
            HomePage homePage = new HomePage(driver);
            homePage.ClickLoginLink();

            try
            {
                LoginPage loginPage = new LoginPage(driver);
            }
            catch
            {
                Assert.True(false, "Login page wasn't opened.");
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
