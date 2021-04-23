using OpenQA.Selenium;
using System;
using System.Text;

namespace TestTask.PageObjects.Booking
{
    public enum CurrencyTypes
    {
        EUR, USD, BYN, RUB
    }

    public enum Languages
    {
        EnglishUS, EnglishUK, Russian, Deutsch
    }

    /// <summary>
    /// Expected driver.Url = "https://www.booking.com/"
    /// </summary>
    public class MainPage
    {
        private IWebDriver driver;

        public IWebElement CurrencyButton => driver.FindElement(By.XPath("//div[@class='bui-group__item']/button[1]"), 10, 20);

        public IWebElement LanguageButton => driver.FindElement(By.XPath("//button[@data-modal-id='language-selection']"), 10, 20); 

        public IWebElement AirTikets => driver.FindElement(By.XPath("//a[@data-decider-header='flights']"), 10, 20);

        public IWebElement LoginButton => driver.FindElement(By.XPath("(//a[contains(@class,'js-header-login-link')])[2]"), 10, 20);

        public IWebElement ExpandProfileMenuButton => driver.FindElement(By.XPath("//a[contains(@aria-describedby,'profile-menu-trigger')]"), 10, 20);

        public IWebElement ManageAccountButton => driver.FindElement(By.XPath("//ul[@class='bui-dropdown-menu__items']/li[1]/a"), 10, 20);

        public IWebElement DestinationTextbox => driver.FindDisplayedElement(By.XPath("//input[contains(@class,'sb-destination__input')]"), 10, 20);

        public IWebElement DateChoosingButton => driver.FindElement(By.XPath("//span[contains(@class,'sb-date-field__icon-btn')]"), 10, 20);

        public IWebElement GuestsSettingsButton => driver.FindElement(By.XPath("//div[@class='xp__input-group xp__guests']"), 10, 20);

        public IWebElement SubmitSearchButton => driver.FindElement(By.XPath("//button[contains(@class,'sb-searchbox__button')]"), 10, 20);

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public MainPage ChangeCurrency(CurrencyTypes targetType)
        {
            CurrencyButton.Click();
            IWebElement targetCurrency = driver.FindElement(By.XPath($"//a[@data-modal-header-async-url-param='changed_currency=1;selected_currency={targetType}']"), 10, 20);
            targetCurrency.Click();
            return this;
        }

        public MainPage ChangeLaguage(Languages targetLanguage)
        {
            string languageCode = getLanguageCode(targetLanguage);
            LanguageButton.Click();
            IWebElement targetLanguageButton = driver.FindElement(By.XPath($"//a[@data-lang='{languageCode}']"), 10, 20);
            targetLanguageButton.Click();
            return this;
        }

        public AirTicketsPage GoToAirTickets()
        {
            AirTikets.Click();
            return new AirTicketsPage(driver);
        }

        public MainPage Login(string email, string password)
        {
            LoginButton.Click();
            LoginPage loginPage = new LoginPage(driver);
            return loginPage.InputEmail(email).InputPassword(password);
        }

        public ManageAccountPage GoToManageAccount()
        {
            ExpandProfileMenuButton.Click();
            ManageAccountButton.Click();
            return new ManageAccountPage(driver);
        }

        public SearchResultsPage RunSearch()
        {
            SubmitSearchButton.Click();
            return new SearchResultsPage(driver);
        }

        public MainPage SelectDates(DateTime departureDate, DateTime arrivalDate)
        {
            DateChoosingButton.Click();
            string departureDateStr = toFormatDate(departureDate);
            IWebElement departureDateButton = driver.FindDisplayedElement(By.XPath($"//td[@data-date='{departureDateStr}']"), 10, 20);
            string arrivalDateStr = toFormatDate(arrivalDate);
            IWebElement arrivalDateButton = driver.FindDisplayedElement(By.XPath($"//td[@data-date='{arrivalDateStr}']"), 10, 20);
            departureDateButton.Click();
            arrivalDateButton.Click();
            return this;
        }

        public MainPage SelectDestination(string destination)
        {
            DestinationTextbox.SendKeys(destination);
            IWebElement firstPropose = driver.FindDisplayedElement(By.XPath("//li[@data-i='0']"), 10, 20);
            firstPropose.Click();
            return this;
        }

        public MainPage SetGuests(int adults, int childrens, int rooms)
        {
            GuestsSettingsButton.Click();
            IWebElement currentNumber = driver.FindDisplayedElement(By.XPath("(//span[@class='bui-stepper__display'])[1]"), 10, 20);
            IWebElement subtractButton = driver.FindDisplayedElement(By.XPath("(//button[contains(@class,'bui-stepper__subtract-button')])[1]"), 10, 20);
            IWebElement addButton = driver.FindDisplayedElement(By.XPath("(//button[contains(@class,'bui-stepper__add-button')])[1]"), 10, 20);
            while (Int32.Parse(currentNumber.Text) < adults)
                addButton.Click();
            while (Int32.Parse(currentNumber.Text) > adults)
                subtractButton.Click();

            currentNumber = driver.FindDisplayedElement(By.XPath("(//span[@class='bui-stepper__display'])[2]"), 10, 20);
            subtractButton = driver.FindDisplayedElement(By.XPath("(//button[contains(@class,'bui-stepper__subtract-button')])[2]"), 10, 20);
            addButton = driver.FindDisplayedElement(By.XPath("(//button[contains(@class,'bui-stepper__add-button')])[2]"), 10, 20);
            while (Int32.Parse(currentNumber.Text) < childrens)
                addButton.Click();
            while (Int32.Parse(currentNumber.Text) > childrens)
                subtractButton.Click();

            currentNumber = driver.FindDisplayedElement(By.XPath("(//span[@class='bui-stepper__display'])[3]"), 10, 20);
            subtractButton = driver.FindDisplayedElement(By.XPath("(//button[contains(@class,'bui-stepper__subtract-button')])[3]"), 10, 20);
            addButton = driver.FindDisplayedElement(By.XPath("(//button[contains(@class,'bui-stepper__add-button')])[3]"), 10, 20);
            while (Int32.Parse(currentNumber.Text) < rooms)
                addButton.Click();
            while (Int32.Parse(currentNumber.Text) > rooms)
                subtractButton.Click();

            return this;
        }

        private string getLanguageCode(Languages language)
        {
            switch (language)
            {
                case Languages.EnglishUS:
                    return "en-us";
                case Languages.EnglishUK:
                    return "en-gb";
                case Languages.Russian:
                    return "ru";
                case Languages.Deutsch:
                    return "de";
                default:
                    throw new NoSuchElementException();
            }
        }

        private string toFormatDate(DateTime date)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(date.Year);
            builder.Append('-');

            if (date.Month < 10)
                builder.Append('0');

            builder.Append(date.Month);
            builder.Append('-');

            if (date.Day < 10)
                builder.Append('0');

            builder.Append(date.Day);
            return builder.ToString();
        }
    }
}
