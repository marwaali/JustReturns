using OpenQA.Selenium;

namespace Task.Pages
{
    public class HomePage
    {
        private IWebDriver _driver;
        private IWebElement _search;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="driver"> webdriver </param>
        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _search = _driver.FindElement(By.Id("search"));  
        }

        public void SearchForProduct(string product)
        {
            _search.SendKeys(product);
            _search.SendKeys(Keys.Enter);
        }

    }
}
