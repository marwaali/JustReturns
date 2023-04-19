using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustReturns.Pages
{
    public  class GooglePage
    {

        private IWebDriver _driver;
        private IWebElement _search;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="driver"> webdriver </param>
        public GooglePage(IWebDriver driver)
        {
            _driver = driver;
            _search = _driver.FindElement(By.Name("q"));
        }

        public void SearchForProduct(string upc)
        {
            _search.SendKeys(upc);
            _search.SendKeys(Keys.Enter);
        }

        public void ClickOnLink(string link)
        {
            IWebElement url = _driver.FindElement(By.XPath($"(//a[contains(@href,'{link}')])[1]"));
            url.Click();
        }
    }
    
}
