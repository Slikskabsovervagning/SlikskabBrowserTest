﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestInitialize]
        public void Setup()
        {
            _driver = new FirefoxDriver(DriverDirectory);
        }

        [TestCleanup]
        public void TearDown()
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
            string url = "file:///C:/Code/3.Semester/browserSlikskab/Index.html";
            _driver.Navigate().GoToUrl(url);

            _driver.FindElement(By.Id("name")).SendKeys("admin");
            _driver.FindElement(By.Id("pass")).SendKeys("123456678");
            _driver.FindElement(By.Id("click")).Click();


            Assert.IsFalse(_driver.Url.EndsWith("MainPage.html"));
            Assert.IsTrue(_driver.Url.EndsWith("Index.html"));
        }
    }
}
