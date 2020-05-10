using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace CreditCards.UITests
{
    public class JavaScriptExamples
    {
        [Fact]
        public void ClickOverlayeredLink()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:44108/JSOverlay.html");
                
                const string script = "return document.getElementById('HiddenLink').innerHTML;";
                IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
             
                var linkText = (string) js.ExecuteScript(script);
                Assert.Equal("Go to Pluralsight", linkText);
            }
        }
    }
}
