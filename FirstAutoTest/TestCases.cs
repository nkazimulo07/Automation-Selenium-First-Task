using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FirstAutoTest
{
    [TestFixture]
    public class TestCases
    {
        private IWebDriver driver;
        private WebDriverWait Wait;
        private const string Url = "http://automationpractice.com/index.php";

        private IWebElement contactUsLink => driver.FindElement(By.CssSelector("div#contact-link"));
        private IWebElement emailInput => driver.FindElement(By.Id("email"));
        private IWebElement subjectHeadingDropdown => driver.FindElement(By.Id("id_contact"));
        private IWebElement orderReference => driver.FindElement(By.Id("id_order"));
        private IWebElement message => driver.FindElement(By.Id("message"));
        private IWebElement submitMessage => driver.FindElement(By.Id("submitMessage"));
        private IWebElement emailSentSuccessfully => driver.FindElement(By.CssSelector("p.alert.alert-success"));
        private IWebElement searchInput => driver.FindElement(By.Name("search_query"));
        private IWebElement searchButton => driver.FindElement(By.Name("submit_search"));
        private IWebElement centerColumn => driver.FindElement(By.Id("center_column"));
        private List<IWebElement> SearchItemNames => new List<IWebElement>(centerColumn.FindElements(By.CssSelector("ul.product.list.grid.row")));
        private IWebElement passwordInput => driver.FindElement(By.Id("passwd"));
        private IWebElement SubmitLogin => driver.FindElement(By.Id("SubmitLogin"));
        private IWebElement myAccountLabel => driver.FindElement(By.CssSelector("h1.page-heading"));
        private IWebElement signInLink => driver.FindElement(By.CssSelector("a.login"));
        private IWebElement signOutLink => driver.FindElement(By.CssSelector("a.logout"));
        bool isLoggedIn = false;

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
        public void GivenRequiredData_WhenSendingEmailViaContactUsPage_ThenReturnEmailSuccesfullySent()
        {
            contactUsLink.Click();
            subjectHeadingDropdown.FindElement(By.XPath("//option[. = 'Customer service']")).Click();
            emailInput.SendKeys("test@test.com");
            orderReference.SendKeys("101");
            message.SendKeys("My first selenium test");
            submitMessage.Click();

            Assert.IsTrue(emailSentSuccessfully.Displayed);
        }

        [Test]
        public void GivenLoginValidCredentials_WhenLoging_ThenReturnSuccessfullyLoggedIn()
        {
            signInLink.Click();
            emailInput.SendKeys("nka@gmail.com");
            passwordInput.SendKeys("Nk@Zee");
            SubmitLogin.Click();
            isLoggedIn = true;
            Assert.IsTrue(myAccountLabel.Text.Equals("MY ACCOUNT"));
        }


        [Test]
        public void GivenItemName_WhenSearchingForItem_ThenReturnSearchResults()
        {
            searchInput.SendKeys("Printed dress");
            searchButton.Click();

            foreach (var itemResult in SearchItemNames)
            {
                Assert.IsTrue(itemResult.Text.Contains("Printed") && itemResult.ToString().Contains("dress"));
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (!isLoggedIn)
            {
                signOutLink.Click();
            }

            driver.Dispose();
        }
    }
}
