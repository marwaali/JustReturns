using OpenQA.Selenium;

namespace Task.Pages
{
    public class SearchPage
    {
        private IWebDriver _driver;
        private IWebElement _productElement;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="driver"> webdriver </param>
        public SearchPage(IWebDriver driver)
        {
            _driver = driver;
            _productElement = _driver.FindElement(By.Id("search"));  
        }

        public void SelectProduct(string product)
        {
            _productElement.Click();
        }

    }
}
