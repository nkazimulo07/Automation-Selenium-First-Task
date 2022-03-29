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
    public class ContactUsTest
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

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
