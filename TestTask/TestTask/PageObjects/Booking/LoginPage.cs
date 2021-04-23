using OpenQA.Selenium;

namespace TestTask.PageObjects.Booking
{
    public class LoginPage
    {
        private IWebDriver driver;

        public IWebElement InputEmailTextbox => this.driver.FindDisplayedElement(By.XPath("//input[@type='email']"), 10, 20);

        public IWebElement InputSubmitButton => this.driver.FindElement(By.XPath("//button[@type='submit']"), 10, 20);

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public LoginSecondPage InputEmail(string email)
        {
            InputEmailTextbox.SendKeys(email);
            InputSubmitButton.Click();
            LoginSecondPage secondPage = new LoginSecondPage(driver);
            return new LoginSecondPage(driver);
        }
    }
}
