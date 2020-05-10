﻿using System.Globalization;
using System.Threading;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string AboutUrl = "http://localhost:44108/Home/About";
        private const string HomeTitle = "Home Page - Credit Cards";

        [Fact, Trait("Category", "Smoke")]
        public void LoadHomePage()
        {
            // smoke test
            using (IWebDriver driver = new ChromeDriver())
            { 
                driver.Navigate().GoToUrl(HomeUrl);
                driver.Manage().Window.Maximize();
                DemoHelper.Pause(); 
                driver.Manage().Window.Minimize();
                DemoHelper.Pause();

                driver.Manage().Window.Size = new System.Drawing.Size(300, 400);
                DemoHelper.Pause();

                driver.Manage().Window.Position = new System.Drawing.Point(1, 1);
                DemoHelper.Pause();
                driver.Manage().Window.Position = new System.Drawing.Point(50, 50);
                DemoHelper.Pause();
                driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
                DemoHelper.Pause();

                driver.Manage().Window.FullScreen();

                DemoHelper.Pause(5000);

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }
         
        [Fact, Trait("Category", "Smoke")]
        public void ReloadHomePage()
        {
            // smoke test
            using (IWebDriver driver = new ChromeDriver())
            { 
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                driver.Navigate().Refresh();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact, Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        { 
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                IWebElement generationTokenElement = driver.FindElement(By.Id("GenerationToken"));
                var initialToken = generationTokenElement.Text;

                DemoHelper.Pause();
                driver.Navigate().GoToUrl(AboutUrl);
                driver.Navigate().Back();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;
                Assert.NotEqual(initialToken, reloadedToken);
            }
        }
        [Fact, Trait("Category", "Smoke")]
        public void ReloadHomePageOnForward()
        { 
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();

                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                driver.Navigate().Back();
                DemoHelper.Pause();

                driver.Navigate().Forward();
                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                // TODO: assert that page was reloaded
            }
        }

        [Fact]
        public void OpenContactFooterLinkNewTab()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                driver.FindElement(By.Id("ContactFooter")).Click();
                DemoHelper.Pause();

                var allTabs = driver.WindowHandles;
                var homePageTab = allTabs[0];
                var contactTab = allTabs[1];

                driver.SwitchTo().Window(contactTab); // driver is now new tab's driver
                Assert.EndsWith("/Home/Contact", driver.Url);
            }
        }
    } 

    public class DemoHelper
    {
        public static void Pause(int secondsToPause = 3000) => Thread.Sleep(secondsToPause);
    }
}
