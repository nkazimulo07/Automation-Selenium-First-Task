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
        private IWebElement emailTextBox => driver.FindElement(By.Id("email"));
        private IWebElement orderReferenceTextBox => driver.FindElement(By.Id("id_order"));
        private IWebElement messagetextArea => driver.FindElement(By.Id("message"));
        private IWebElement submitMessage => driver.FindElement(By.Id("submitMessage"));
        private IWebElement emailSentSuccessfullyMessage => driver.FindElement(By.CssSelector("p.alert.alert-success"));
        private IWebElement searchTextBox => driver.FindElement(By.Name("search_query"));
        private IWebElement searchButton => driver.FindElement(By.Name("submit_search"));
        private IWebElement centerColumn => driver.FindElement(By.Id("center_column"));
        private List<IWebElement> SearchItemNames => new List<IWebElement>(centerColumn.FindElements(By.CssSelector("ul.product.list.grid.row")));
        private IWebElement passwordTextBox => driver.FindElement(By.Id("passwd"));
        private IWebElement SubmitLogin => driver.FindElement(By.Id("SubmitLogin"));
        private IWebElement myAccountLabel => driver.FindElement(By.CssSelector("h1.page-heading"));
        private IWebElement signInLink => driver.FindElement(By.CssSelector("a.login"));
        private IWebElement signOutLink => driver.FindElement(By.CssSelector("a.logout"));
        private IWebElement subjectHeading => driver.FindElement(By.Id("id_contact"));
        private SelectElement subjectHeadingDropdown => new SelectElement(subjectHeading);
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
        public void GivenRequiredData_WhenSendingEmailViaContactUsPage_ThenEmailSentSuccessfulMessageReturned()
        {
            contactUsLink.Click();
            subjectHeadingDropdown.SelectByText("Customer service");
            emailTextBox.SendKeys("test@test.com");
            orderReferenceTextBox.SendKeys("101");
            messagetextArea.SendKeys("My first selenium test");
            submitMessage.Click();

            Assert.IsTrue(emailSentSuccessfullyMessage.Displayed);
        }

        [Test]
        public void GivenLoginValidCredentials_WhenLoging_ThenMyAccountPageReturned()
        {
            signInLink.Click();
            emailTextBox.SendKeys("nka@gmail.com");
            passwordTextBox.SendKeys("Nk@Zee");
            SubmitLogin.Click();
            isLoggedIn = true;

            Assert.IsTrue(myAccountLabel.Text.Equals("MY ACCOUNT"));
        }

        [Test]
        public void GivenItemName_WhenSearchingForItem_ThenSearchResultsReturned()
        {
            searchTextBox.SendKeys("Printed dress");
            searchButton.Click();

            foreach (var itemResult in SearchItemNames)
            {
                Assert.IsTrue(itemResult.Text.Contains("Printed") && itemResult.ToString().Contains("dress"));
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (isLoggedIn)
            {
                signOutLink.Click();
            }

            driver.Dispose();
        }
    }
}
