﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace CreditCards.UITests
{
    public class CreditCardApplicationShould
    { 
        private const string HomeUrl = "http://localhost:44108/";
        private const string ApplyUrl = "http://localhost:44108/Apply";

        [Fact]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement applyLink = driver.FindElement(By.Name("ApplyLowRate"));
                applyLink.Click();

                DemoHelper.Pause();
                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause(11000);

                IWebElement applyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                applyLink.Click();
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
    }
}
