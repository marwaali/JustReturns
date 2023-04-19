using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Task
{
    public class DriverOperations
    {
        private IWebDriver _driver;

        public DriverOperations(IWebDriver driver)
        {
            _driver = driver;
        }

        public void CloseDriver()
        {
            _driver.Dispose();
        }

        public void ScrollToElement(IWebElement element)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView();", element);
            Thread.Sleep(1000);
        }

        public void WaitForElementToBeInvisible(IWebElement element, int time)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(time));
            wait.Until(d => !element.Displayed);
        }

        public bool IsElementPresent(string elementLocator)
        {
            try
            {
                return _driver.FindElement(By.XPath(elementLocator)).Displayed;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void JSClick(IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }
    }
}