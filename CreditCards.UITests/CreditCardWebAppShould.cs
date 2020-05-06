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

        [Fact, Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        { 
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();
                driver.Navigate().GoToUrl(AboutUrl);
                driver.Navigate().Back();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                // TODO: assert that page was reloaded
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
    }

    public class DemoHelper
    {
        public static void Pause(int secondsToPause = 3000) => Thread.Sleep(secondsToPause);
    }
}
