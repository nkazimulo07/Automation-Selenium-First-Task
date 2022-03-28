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
        private IWebElement searchInput => driver.FindElement(By.Name("search_query"));
        private IWebElement searchButton => driver.FindElement(By.Name("submit_search"));
        private IWebElement centerColumn => driver.FindElement(By.Id("center_column"));
        private List<IWebElement> SearchItemNames => new List<IWebElement>(centerColumn.FindElements(By.CssSelector("ul.product.list.grid.row")));
        private IWebElement emailInput => driver.FindElement(By.Id("email"));
        private IWebElement subjectHeadingDropdown => driver.FindElement(By.Id("id_contact"));
        private IWebElement orderReference => driver.FindElement(By.Id("id_order"));
        private IWebElement message => driver.FindElement(By.Id("message"));
        private IWebElement contactUsLink => driver.FindElement(By.Id("contact-link"));
        private IWebElement submitMessage => driver.FindElement(By.Id("submitMessage"));
        private IWebElement emailSentSuccessfully => driver.FindElement(By.CssSelector("p.alert.alert-success"));
        private IWebElement passwordInput => driver.FindElement(By.Id("passwd"));
        private IWebElement SubmitLogin => driver.FindElement(By.Id("SubmitLogin"));
        private IWebElement myAccountList => driver.FindElement(By.CssSelector("ul.myaccount-link-list"));
        private IWebElement signInLink => driver.FindElement(By.LinkText("Sign in"));
       
        [SetUp]
        public void SetUp()
        {
            using(IWebDriver newDriver = new ChromeDriver())
            {
                driver = newDriver;
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(Url);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            }
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
        public void GivenItemName_WhenSearchingForItem_ThenReturnSearchResults()
        {
            searchInput.SendKeys("Printed dress");
            searchButton.Click();

            foreach (var itemResult in SearchItemNames)
            {
                Assert.IsTrue(itemResult.Text.Contains("Printed") && itemResult.ToString().Contains("dress"));
            }
        }

        [Test]
        public void GivenLoginData_WhenLoging_ThenReturnSuccessfullyLoggedIn()
        {
            signInLink.Click();
            emailInput.SendKeys("nka@gmail.com");
            passwordInput.SendKeys("Nk@Zee");
            SubmitLogin.Click();
            Assert.IsTrue(myAccountList.Displayed);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
