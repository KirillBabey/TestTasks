using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TestTask.PageObjects.Booking;

namespace TestTask_Tests
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            this.driver = new ChromeDriver();
            this.driver.Url = "https://www.booking.com";
        }

        [Test, Description("Ñalling the ChangeLanguage method, the state of the language on the main page must be changed to the specified")]
        public void LanguageChangingTest()
        {
            MainPage mainPage = new MainPage(this.driver);
            mainPage.ChangeLaguage(Languages.EnglishUK);
            Assert.IsTrue(mainPage.LanguageButton.Text == "Choose your language. Your current language is English (UK)");
        }

        [Test, Description("Ñalling the ChangeCurrency method, the state of the currency on the main page must be changed to the specified")]
        public void CurrencyChangingTest()
        {
            MainPage mainPage = new MainPage(this.driver);
            mainPage.ChangeCurrency(CurrencyTypes.EUR);
            string currencyType = mainPage.CurrencyButton.Text.Substring(0, 3);
            Assert.IsTrue(currencyType == "EUR");
        }

        [Test, Description("Transition to AirTicketsPage, special elements of this page must be displayed")]
        public void TransitionToAirTicketsPageTest()
        {
            MainPage mainPage = new MainPage(this.driver);
            AirTicketsPage ticketsPage = mainPage.GoToAirTickets();
            Assert.IsTrue(ticketsPage.SelectOriginAirportButton.Displayed);
        }

        [Test, Description("ManageAccountPage accessibility test")]
        public void ManageAccountWithAccessibilityTest()
        {
            MainPage mainPage = new MainPage(this.driver).Login("babeyfakemail@gmail.com", "MegaPassword1");
            ManageAccountPage accountPage = mainPage.GoToManageAccount();
            Assert.IsTrue(accountPage.PersonalDetailsButton.Displayed);
        }

        [Test, Description("Ñhecking the correspondence of the filtering results to the set parameters")]
        public void FiltersTest()
        {
            MainPage mainPage = new MainPage(this.driver);
            mainPage.SelectDates(DateTime.Now.AddDays(7), DateTime.Now.AddDays(9));
            mainPage.SelectDestination("Ìèíñê");
            mainPage.SetGuests(2, 1, 1);
            SearchResultsPage resultsPage = mainPage.RunSearch();

            foreach (IWebElement element in resultsPage.ResultLocations)
                if (!element.Text.Contains("Ìèíñê"))
                    Assert.Fail("Not all filtered results meet the setted filter parametres");
            foreach (IWebElement element in resultsPage.ResultCheckInInfo)
                if (element.Text != "2 íî÷è, 2 âçðîñëûõ, 1 ðåáåíîê")
                    Assert.Fail("Not all filtered results meet the setted filter parametres");

            Assert.Pass();
        }

        [TearDown]
        public void CloseBrowser()
        {
            this.driver.Quit();
        }
    }
}