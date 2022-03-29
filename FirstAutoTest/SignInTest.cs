using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace FirstAutoTest
{
    [TestFixture]
    public class SignInTest
    {
        private IWebDriver driver;
        private WebDriverWait Wait;
        private const string Url = "http://automationpractice.com/index.php";

        private IWebElement passwordInput => driver.FindElement(By.Id("passwd"));
        private IWebElement SubmitLogin => driver.FindElement(By.Id("SubmitLogin"));
        private IWebElement myAccountLabel => driver.FindElement(By.CssSelector("h1.page-heading"));
        private IWebElement signInLink => driver.FindElement(By.CssSelector("a.login"));
        private IWebElement signOutLink => driver.FindElement(By.CssSelector("a.logout"));
        private IWebElement emailInput => driver.FindElement(By.Id("email"));

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }

        [Test]
        public void GivenLoginValidCredentials_WhenLoging_ThenReturnSuccessfullyLoggedIn()
        {
            signInLink.Click();
            emailInput.SendKeys("nka@gmail.com");
            passwordInput.SendKeys("Nk@Zee");
            SubmitLogin.Click();
            Assert.IsTrue(myAccountLabel.Text.Equals("MY ACCOUNT"));
        }

        [TearDown]
        public void TearDown()
        {
            signOutLink.Click();
            driver.Dispose();
        }
    }
}
