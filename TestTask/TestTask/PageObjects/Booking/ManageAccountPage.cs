using OpenQA.Selenium;

namespace TestTask.PageObjects.Booking
{
    public class ManageAccountPage
    {
        private IWebDriver driver;

        public IWebElement PersonalDetailsButton => this.driver.FindElement(By.XPath("//li[@data-ga-label='Category: personal_details']"), 10, 20);
        
        public ManageAccountPage(IWebDriver driver)
        {
            this.driver = driver;
        }

    }
}
