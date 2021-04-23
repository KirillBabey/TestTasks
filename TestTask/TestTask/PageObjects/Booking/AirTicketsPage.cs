using OpenQA.Selenium;

namespace TestTask.PageObjects.Booking
{
    public class AirTicketsPage
    {
        private IWebDriver driver;

        public IWebElement SelectOriginAirportButton => this.driver.FindElement(By.XPath("//div[contains(@id,'-origin-airport-display')]"), 10, 20);

        public AirTicketsPage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
