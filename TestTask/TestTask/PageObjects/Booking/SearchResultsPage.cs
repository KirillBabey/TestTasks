using OpenQA.Selenium;
using System.Collections.Generic;

namespace TestTask.PageObjects.Booking
{
    public class SearchResultsPage
    {
        private IWebDriver driver;

        public IList<IWebElement> ResultLocations => this.driver.FindElements(By.XPath($"//a[@class='bui-link']"));

        public IList<IWebElement> ResultCheckInInfo => this.driver.FindElements(By.XPath($"//div[@class='bui-price-display__label prco-inline-block-maker-helper']"));

        public SearchResultsPage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
