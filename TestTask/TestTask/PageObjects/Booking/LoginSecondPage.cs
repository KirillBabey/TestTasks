using OpenQA.Selenium;

namespace TestTask.PageObjects.Booking
{
    public class LoginSecondPage
    {
        private IWebDriver driver;

        public IWebElement InputPasswordTextbox => this.driver.FindDisplayedElement(By.XPath("//input[@type='password']"), 10, 20);

        public IWebElement InputSubmitButton => this.driver.FindElement(By.XPath("//button[@type='submit']"), 10, 20);

        public LoginSecondPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public MainPage InputPassword(string password)
        {
            InputPasswordTextbox.SendKeys(password);
            InputSubmitButton.Click();
            return new MainPage(driver);
        }
    }
}
