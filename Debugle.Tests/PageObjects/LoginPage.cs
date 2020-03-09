using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Debugle.Tests
{
    public class LoginPage
    {
        private IWebDriver _driver;
        By _userLoginLocator = By.Id("UserEmail");
        By _userPasswordLocator = By.Id("UserPassword");
        By _loginButtonLocator = By.ClassName("button");
        By _errorMessageDivLocator = By.Id("flashMessage");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;

            if (_driver.FindElements(By.XPath("//h1[text()='Welcome Back!']")).Count == 0)
            {
                throw new InvalidOperationException("It is not Login page");
            }
        }

        public void TypeUserLogin(string userLogin)
        {
            _driver.FindElement(_userLoginLocator).SendKeys(userLogin);
        }

        public void TypeUserPassword(string userPassword)
        {
            _driver.FindElement(_userPasswordLocator).SendKeys(userPassword);
        }

        public void ClickLoginButton()
        {
            _driver.FindElement(_loginButtonLocator).Click();
        }

        public string GiveErrorMessageText()
        {
            return _driver.FindElement(_errorMessageDivLocator).Text;
        }

        public string GiveEmailFieldValidationMesssageText()
        {
            return _driver.FindElement(_userLoginLocator).GetAttribute("validationMessage");
        }

        public string GivePasswordFieldValidationMesssageText()
        {
            return _driver.FindElement(_userPasswordLocator).GetAttribute("validationMessage");
        }
    }

}
