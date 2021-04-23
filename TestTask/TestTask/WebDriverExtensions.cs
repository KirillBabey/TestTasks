using OpenQA.Selenium;
using System;
using System.Threading;

namespace TestTask
{
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Searching for the first IWebElement by specified search method 
        /// and specified maximum search time. Returns WebDriverTimeoutException if time runs out
        /// if maximum wait time = 0 works as basic FindElement method
        /// </summary>
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds, int searchPeriodInMilliseconds)
        {
            if (timeoutInSeconds == 0)
            {
                return driver.FindElement(by);
            }

            IWebElement element;
            DateTime endWaitTime = DateTime.Now.AddSeconds(timeoutInSeconds);

            while (DateTime.Now < endWaitTime)
            {
                try
                {
                    element = driver.FindElement(by);
                    return element;
                }
                catch (StaleElementReferenceException)
                {
                }
                catch (NoSuchElementException)
                {
                }

                Thread.Sleep(searchPeriodInMilliseconds);
            }

            throw new WebDriverTimeoutException();
        }

        /// <summary>
        /// Searching for the first displayed IWebElement by specified search method 
        /// and specified maximum search time. Returns WebDriverTimeoutException if time runs out
        /// </summary>
        public static IWebElement FindDisplayedElement(this IWebDriver driver, By by, int timeoutInSeconds, int searchPeriodInMilliseconds)
        {
            if (timeoutInSeconds <= 0)
            {
                return driver.FindElement(by);
            }

            IWebElement element;
            DateTime endWaitTime = DateTime.Now.AddSeconds(timeoutInSeconds);

            while (DateTime.Now < endWaitTime)
            {
                try
                {
                    element = driver.FindElement(by);
                    if (element.Displayed == true)
                    {
                        return element;
                    }
                }
                catch (StaleElementReferenceException)
                {
                }
                catch (NoSuchElementException)
                {
                }

                Thread.Sleep(searchPeriodInMilliseconds);
            }

            throw new WebDriverTimeoutException();
        }

        /// <summary>
        /// Return false if the IWebElement does not displayed within the specified time, else returns true
        /// </summary>
        public static bool Displayed(this IWebElement element, int timeoutInMilliseconds, int searchPeriodInMilliseconds)
        {
            if (timeoutInMilliseconds > 0)
            {
                DateTime endWaitTime = DateTime.Now.AddMilliseconds(timeoutInMilliseconds);

                while (DateTime.Now < endWaitTime)
                {
                    try
                    {
                        if (element.Displayed == true)
                        {
                            return true;
                        }
                    }
                    catch (StaleElementReferenceException)
                    {
                    }

                    Thread.Sleep(searchPeriodInMilliseconds);
                }

                return false;
            }
            else
            {
                return element.Displayed;
            }
        }
    }
}
