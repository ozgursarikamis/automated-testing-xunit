using System;
using System.IO;
using System.Threading;
using ApprovalTests;
using ApprovalTests.Reporters;
using CreditCards.UITests.PageObjectModel;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
                var homePage = new HomePage(driver);
                string initialToken = homePage.GenerationToken;

                driver.Navigate().GoToUrl(AboutUrl);
                driver.Navigate().Back();

                homePage.EnsurePageLoaded();

                string reloadedToken = homePage.GenerationToken;
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

        [Fact]
        [Obsolete]
        public void AlertIfLiveChatClosed()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                driver.FindElement(By.Id("LiveChat")).Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

                Assert.Equal("Live chat is currently closed.", alert.Text);
                DemoHelper.Pause();
                alert.Accept(); // clicking OK
            }
        }

        [Fact]
        [Obsolete]
        public void NotNavigateToAboutUsWhenCancelClicked()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                Assert.Equal(HomeTitle, driver.Title);

                driver.FindElement(By.Id("LearnAboutUs")).Click();
                DemoHelper.Pause();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());

                alertBox.Dismiss(); // clicking cancel

                Assert.Equal(HomeTitle, driver.Title);
            }
        }
        [Fact]
        public void NotDisplayCookieUseMessage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                driver.Manage().Cookies.AddCookie(new Cookie("acceptedCookies", "true"));
                driver.Navigate().Refresh();

                var message = driver.FindElements(By.Id("CookiesBeingUsed"));

                Assert.Empty(message);

                // read cookie:
                Cookie cookie = driver.Manage().Cookies.GetCookieNamed("acceptedCookies");
                Assert.Equal("true", cookie.Value);

                driver.Manage().Cookies.DeleteCookieNamed("acceptedCookies");
                driver.Navigate().Refresh();
                 
                Assert.NotNull(driver.FindElement(By.Id("CookiesBeingUsed")));
            }
        }

        [Fact]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void RenderAboutPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutUrl);

                ITakesScreenshot screenShotDriver = (ITakesScreenshot) driver;
                Screenshot screenShot = screenShotDriver.GetScreenshot();
                screenShot.SaveAsFile("aboutpage.bmp", ScreenshotImageFormat.Bmp);

                FileInfo file = new FileInfo("aboutpage.bmp");
                Approvals.Verify(file);
            }
        }
    } 

    public class DemoHelper
    {
        public static void Pause(int secondsToPause = 3000) => Thread.Sleep(secondsToPause);
    }
}
