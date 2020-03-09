using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Debugle.Tests
{
    public class LoginPageTests
    {
        private const string _correctLogin = "deveducation1@i.ua";
        private const string _correctPassword = "Qwerty123";
        private const string _loginPageUrl = "https://debugle.com/login";

        [Fact]
        public void CheckThatItIsPossibleToOpenLoginPage()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_loginPageUrl);
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

        [Fact]
        public void CheckThatItIsPossibleToLoginWithCorrectLoginAndPassword()
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
                Assert.True(false, "Login wasn't successful.");
            }
            finally
            {
                driver.Quit();
            }
        }

        [Theory]
        [InlineData("somepassword", "Please fill out this field.")]
        [InlineData("", "Please fill out this field.")]
        public void CheckThatItIsNotPossibleToLoginWithEmptyLoginAndFilledPassword(string password, string expected)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_loginPageUrl);
            LoginPage loginPage = new LoginPage(driver);
            loginPage.TypeUserPassword(password);
            loginPage.ClickLoginButton();

            string actual = loginPage.GiveEmailFieldValidationMesssageText();
            driver.Quit();

            Assert.True(expected == actual);
        }

        [Theory]
        [InlineData(_correctLogin, "Please fill out this field.")]
        [InlineData(_correctLogin + "incorrect", "Please fill out this field.")]
        public void CheckThatItIsNotPossibleToLoginWithEmptyPassword(string email, string expected)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_loginPageUrl);
            LoginPage loginPage = new LoginPage(driver);
            loginPage.TypeUserLogin(email);
            loginPage.ClickLoginButton();

            string actual = loginPage.GivePasswordFieldValidationMesssageText();
            driver.Quit();

            Assert.True(expected == actual);
        }

        [Theory]
        [InlineData(_correctLogin+"incorrect", _correctPassword, "The email and password don't match!")]
        [InlineData(_correctLogin + "incorrect", _correctPassword + "incorrect", "The email and password don't match!")]
        public void CheckThatItIsNotPossibleToLoginWithInvalidEmailOrInvalidPassword(string login, string password, string expected)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_loginPageUrl);
            LoginPage loginPage = new LoginPage(driver);
            loginPage.TypeUserLogin(login);
            loginPage.TypeUserPassword(password);
            loginPage.ClickLoginButton();

            string actual = loginPage.GiveErrorMessageText();
            driver.Quit();

            Assert.True(expected == actual);
        }

        [Theory]
        [InlineData("notemail", "somepassword", "Please include an '@' in the email address. 'notemail' is missing an '@'.")]
        [InlineData("notemail", "", "Please include an '@' in the email address. 'notemail' is missing an '@'.")]
        [InlineData("notemail@", "somepassword", "Please enter a part following '@'. 'notemail@' is incomplete.")]
        [InlineData("notemail@", "", "Please enter a part following '@'. 'notemail@' is incomplete.")]
        public void CheckThatItIsNotPossibleToLoginWithEmailInInvalidFormat(string login, string password, string expected)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_loginPageUrl);
            LoginPage loginPage = new LoginPage(driver);
            loginPage.TypeUserLogin(login);
            loginPage.TypeUserPassword(password);
            loginPage.ClickLoginButton();

            string actual = loginPage.GiveEmailFieldValidationMesssageText();
            driver.Quit();

            Assert.True(expected == actual, actual);
        }
    }
}
