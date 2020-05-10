using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace CreditCards.UITests.PageObjectModel
{
    public class HomePage
    {
        private readonly IWebDriver Driver;

        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public ReadOnlyCollection<IWebElement> ProductCells
        {
            get
            {
                return Driver.FindElements(By.TagName("td"));
            }
        }
    }
}
