using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using PageObjectModel.Source.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectModel.Tests
{
    public class AutomationTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void InitScript()
        {

            _driver = new ChromeDriver();
        }

        [Test]
        public void LoginWithAlphanumericPassword()
        {
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);

            _driver.Navigate().GoToUrl("https://demoqa.com/books");
            _driver.Manage().Window.Size = new System.Drawing.Size(900, 900);
            _driver.FindElement(By.Id("login")).Click();
            _driver.FindElement(By.Id("userName")).SendKeys("user");
            _driver.FindElement(By.Id("password")).SendKeys("BxUSHkVj7QSh3yN!");
            
            _driver.FindElement(By.Id("newUser")).Click();

            _driver.FindElement(By.Id("userName")).SendKeys("user");
            _driver.FindElement(By.Id("firstname")).Click();
            _driver.FindElement(By.Id("firstname")).SendKeys("user");
            _driver.FindElement(By.Id("lastname")).SendKeys("user");
            _driver.FindElement(By.Id("password")).SendKeys("5Bh24JidJyX6");

            _driver.SwitchTo().Frame(0);

            _driver.FindElement(By.CssSelector(".recaptcha-checkbox-border")).Click();
            _driver.SwitchTo().DefaultContent();
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

            _driver.FindElement(By.Id("register")).Click();

            // ovaj alert se NEĆE pojaviti i test će PASTI jer je u pitanju BUG
            _driver.SwitchTo().Alert().Accept();

            _driver.Close();
        }

        [Test]
        public void CannotAddAlreadyAddedBook()
        {
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);

            _driver.Navigate().GoToUrl("https://demoqa.com/books");
            _driver.Manage().Window.Maximize();
            _driver.FindElement(By.Id("login")).Click();
            _driver.FindElement(By.Id("userName")).SendKeys("user");
            _driver.FindElement(By.Id("password")).SendKeys("BxUSHkVj7QSh3yN!");
            _driver.FindElement(By.Id("login")).Click();

            _driver.FindElement(By.LinkText("Git Pocket Guide")).Click();
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            _driver.FindElement(By.CssSelector(".text-right > #addNewRecordButton")).Click();
            
            Thread.Sleep(500);

            Assert.That(_driver.SwitchTo().Alert().Text, Is.EqualTo("Book added to your collection."));
            _driver.SwitchTo().Alert().Accept();

            _driver.FindElement(By.CssSelector(".col-md-6")).Click();

            // knjiga je dodana, dugme ne treba biti vidljivo, test PADA jer je u pitanju BUG

            var btn = _driver.FindElement(By.Id("addNewRecordButton"));
            Assert.IsFalse(btn.Displayed);

            _driver.Close();
        }

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
        }
    }
}