using System.Threading;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        [Fact, Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            // smoke test
            using (IWebDriver driver = new ChromeDriver())
            {
                const string homeUrl = "http://localhost:44108/";
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();
                var pageTitle = driver.Title;
                Assert.Equal("Home Page - Credit Cards", driver.Title);
                Assert.Equal(homeUrl, driver.Url);
            }
        }
    }

    public class DemoHelper
    {
        public static void Pause(int secondsToPause = 3000) => Thread.Sleep(secondsToPause);
    }
}
