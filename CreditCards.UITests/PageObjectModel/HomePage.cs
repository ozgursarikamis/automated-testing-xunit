using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace CreditCards.UITests.PageObjectModel
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private const string PageUrl = "http://localhost:44108/";
        private const string PageTitle = "Home Page - Credit Cards";

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public ReadOnlyCollection<(string name, string interestRate)> Products
        {
            get
            {
                var products = new List<(string name, string interestRate)>();
                var productCells = _driver.FindElements(By.TagName("td"));

                for (int i = 0; i < productCells.Count; i+=2)
                {
                    string name = productCells[i].Text;
                    string interestRate = productCells[i + 1].Text;
                    products.Add((name, interestRate));
                }

                return products.AsReadOnly();
            }
        }

        public string GenerationToken => _driver.FindElement(By.Id("GenerationToken")).Text;

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl(PageUrl);
            EnsurePageLoaded();
        }

        public void EnsurePageLoaded()
        {
            var pageHasLoaded = _driver.Url == PageUrl && _driver.Title == PageTitle;
            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load: {_driver.PageSource}");
            }
        }
    }
}
