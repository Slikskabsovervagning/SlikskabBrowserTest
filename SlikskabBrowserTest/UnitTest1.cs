using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace SlikskabBrowserTest
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string DriverDirectory = "C:\\Code\\3.Semester\\ChromeDriver";

        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext test)
        {
            _driver = new FirefoxDriver(DriverDirectory);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void LoginWithRightCredentials_RedirectsToMainPage()
        {
            string url = "file:///C:/Code/3.Semester/browserSlikskab/Index.html";
            _driver.Navigate().GoToUrl(url);

            _driver.FindElement(By.Id("name")).SendKeys("admin");
            _driver.FindElement(By.Id("pass")).SendKeys("1234");
            _driver.FindElement(By.Id("click")).Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until((r) => r.Url.EndsWith("MainPage.html"));
            Assert.IsTrue(_driver.Url.EndsWith("MainPage.html"));

        }

        [TestMethod]
        public void LogInWithWrongCredentials()
        {
            string url = "https://updog-slikskab.azurewebsites.net/";
            _driver.Navigate().GoToUrl(url);

            _driver.FindElement(By.Id("name")).SendKeys("admin");
            _driver.FindElement(By.Id("pass")).SendKeys("123456678");
            _driver.FindElement(By.Id("click")).Click();


            Assert.IsFalse(_driver.Url.EndsWith("MainPage.html"));
            Assert.IsTrue(_driver.Url.EndsWith(".net/"));
        }

        [TestMethod]
        public void Filter_ShouldIncludeSearchedDate()
        {
            string url = "https://updog-slikskab.azurewebsites.net/MainPage.html";
            _driver.Navigate().GoToUrl(url);

            _driver.FindElement(By.Id("inputDay")).SendKeys("5");
            _driver.FindElement(By.Id("inputMonth")).SendKeys("5");
            _driver.FindElement(By.Id("inputYear")).SendKeys("2021");
            _driver.FindElement(By.Id("filterButton")).Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            var rows = wait.Until((d) => d.FindElement(By.ClassName("reading-row")));
            Assert.IsTrue(rows.Text.Contains("05-05"));

        }

        [TestMethod]
        public void Check_SensorLocks_atSensor1()
        {
            string url = "https://updog-slikskab.azurewebsites.net/MainPage.html";
            _driver.Navigate().GoToUrl(url);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            var image = wait.Until((d)=>d.FindElement(By.Id("1")));
            _driver.FindElement(By.Id("closeLock")).Click();
            Assert.IsTrue(image.GetAttribute("src").EndsWith("locked.gif"));
            _driver.FindElement(By.Id("openLock")).Click();
            Assert.AreEqual(_driver.FindElement(By.Id("1")).GetAttribute("src"), image.GetAttribute("src"));

        }

        [TestMethod]
        public void Change_SensorName()
        {
            string url = "https://updog-slikskab.azurewebsites.net/MainPage.html";
            _driver.Navigate().GoToUrl(url);
            WebDriverWait wait = new WebDriverWait(_driver,TimeSpan.FromSeconds(5));
            var input = wait.Until((d)=>d.FindElement(By.Id("nameInput1")));
            input.SendKeys("LivingRoom");
            _driver.FindElement(By.Id("nameButton1")).Click();
            var name = _driver.FindElement(By.Id("name1")).Text;
            Assert.AreEqual("Sensor: LivingRoom", name);
            
        }
    }
}
