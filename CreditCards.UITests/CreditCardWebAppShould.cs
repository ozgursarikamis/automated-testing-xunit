using System.Threading;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string HomeTitle = "Home Page - Credit Cards";

        [Fact, Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            // smoke test
            using (IWebDriver driver = new ChromeDriver())
            { 
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause(); 
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
    }

    public class DemoHelper
    {
        public static void Pause(int secondsToPause = 3000) => Thread.Sleep(secondsToPause);
    }
}
