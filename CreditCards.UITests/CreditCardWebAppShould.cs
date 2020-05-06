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
                driver.Navigate().GoToUrl("http://localhost:44108/");
            }
        }
    }
}
