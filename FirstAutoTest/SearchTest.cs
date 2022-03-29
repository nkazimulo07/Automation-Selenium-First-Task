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
    public class SearchTest
    {
        private IWebDriver driver;
        private WebDriverWait Wait;
        private const string Url = "http://automationpractice.com/index.php";

        private IWebElement searchInput => driver.FindElement(By.Name("search_query"));
        private IWebElement searchButton => driver.FindElement(By.Name("submit_search"));
        private IWebElement centerColumn => driver.FindElement(By.Id("center_column"));
        private List<IWebElement> SearchItemNames => new List<IWebElement>(centerColumn.FindElements(By.CssSelector("ul.product.list.grid.row")));

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
            driver.Dispose();
        }
    }
}
