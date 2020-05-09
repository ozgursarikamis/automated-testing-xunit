﻿using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;

namespace CreditCards.UITests
{
    public class CreditCardApplicationShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string ApplyUrl = "http://localhost:44108/Apply";

        private readonly ITestOutputHelper _output;

        public CreditCardApplicationShould(ITestOutputHelper output)
        {
            _output = output;
        }

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
                DemoHelper.Pause();

                IWebElement carouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselNext.Click();
                DemoHelper.Pause(1000); // allow carousel to scroll

                IWebElement applyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                applyLink.Click();
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to HomeUrl");
                driver.Navigate().GoToUrl(HomeUrl);

                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element using explicit way");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));

                //IWebElement carouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                //carouselNext.Click();
                //DemoHelper.Pause(1000); // allow carousel to scroll
                //carouselNext.Click();
                //DemoHelper.Pause(1000); // allow carousel to scroll

                IWebElement applyLink = wait.Until(d => d.FindElement(By.ClassName("customer-service-apply-now")));

                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} " +
                                  $"Finding element Displayed={applyLink.Displayed} Enabled={applyLink.Enabled}");
                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                ReadOnlyCollection<IWebElement> tableCells =
                    driver.FindElements(By.TagName("td"));

                Assert.Equal("Easy Credit Card", tableCells[0].Text);
                Assert.Equal("20% APR", tableCells[1].Text);

                Assert.Equal("Silver Credit Card", tableCells[2].Text);
                Assert.Equal("18% APR", tableCells[3].Text);

                Assert.Equal("Gold Credit Card", tableCells[4].Text);
                Assert.Equal("17% APR", tableCells[5].Text);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink = driver.FindElement(By.PartialLinkText(" - Apply Now"));
                randomGreetingApplyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting_Using_XPATH()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink =
                    driver.FindElement(By.XPath("/html/body/div/div[4]/div/div/p/a"));
                randomGreetingApplyLink.Click();
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplciation()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement carouselNext =
                    driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselNext.Click();

                WebDriverWait wait =
                    new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                IWebElement applyLink = wait.Until(d =>
                    d.FindElement(By.LinkText("Easy: Apply Now!")));
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication_Prebuilt_Conditions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(11));

                var element = By.LinkText("Easy: Apply Now!");

                //waits until the 'element' is clicked. if not, there will be timeout.
                IWebElement applyLink = wait.Until(
                    ExpectedConditions.ElementToBeClickable(element)
                );
                applyLink.Click();

                DemoHelper.Pause();
                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }


        [Fact]
        public void BeSubmittedWhenValid()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(ApplyUrl);

                IWebElement firstNameField = driver.FindElement(By.Id("FirstName"));
                // firstNameField.Text = "Sarah"; // <-- it's readonly.
                firstNameField.SendKeys("Sarah");
                // DemoHelper.Pause();
                driver.FindElement(By.Id("LastName")).SendKeys("Smith");
                // DemoHelper.Pause();
                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("123456-A");
                // DemoHelper.Pause();
                driver.FindElement(By.Id("Age")).SendKeys("18");
                // DemoHelper.Pause();
                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");

                IWebElement singleRadio = driver.FindElement(By.Id("Single"));
                singleRadio.Click();

                IWebElement businessSourceElement = driver.FindElement(By.Id("BusinessSource"));
                SelectElement businessSource = new SelectElement(businessSourceElement);

                // Check default selected option is correct
                Assert.Equal("I'd Rather Not Say", businessSource.SelectedOption.Text);

                // get all the available options:
                foreach (var option in businessSource.Options)
                    _output.WriteLine($"Value: {option.GetAttribute("value")} Text: {option.Text}");

                Assert.Equal(5, businessSource.Options.Count);
                businessSource.SelectByText("Internet Search");
                DemoHelper.Pause();

                businessSource.SelectByValue("Email");
                DemoHelper.Pause();

                businessSource.SelectByIndex(4);
                DemoHelper.Pause();

                DemoHelper.Pause(5000);

            }
        }
    }
}
