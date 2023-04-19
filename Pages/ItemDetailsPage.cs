using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Task.Pages
{
    public class ItemDetailsPage
    {
        private IWebDriver _driver;
        private DriverOperations _op;
        private IWebElement _description;
        private IList<IWebElement> _highlights;
        private IList<IWebElement> _specs;
        private IWebElement _seeMore;
        private IWebElement _upc;
        private IWebElement _seeMoreImages;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="driver"> webdriver </param>
        public ItemDetailsPage(IWebDriver driver)
        {
            _driver = driver;
            _op = new DriverOperations(driver);
            //_description = _driver.FindElement(By.XPath("//h3[text()='Description']/following-sibling::div"));
            //_highlights = _driver.FindElements(By.XPath("//h3[text()='Highlights']/following-sibling::ul//span"));
            _specs = _driver.FindElements(By.XPath("//h3[text()='Specifications']/following-sibling::div"));
            _seeMore = _driver.FindElement(By.XPath("//button[text()='Show more']"));  
            _upc = _driver.FindElement(By.XPath("//b[text()='UPC']/./.."));
            //_seeMoreImages = _driver.FindElement(By.XPath("//span[contains(@aria-label,'open modal')]")); 
        }

        public string GetDescription()
        {
            return _description.Text;
        }

        public List<string> GetHighlights()
        {
            List<string> highlights = new List<string>();
            foreach (var highlight in _highlights)
            {
                highlights.Add(highlight.Text) ;
            }
            string str = String.Join(" - ", highlights);
            return highlights;
        }

        public List<string> GetSpecs()
        {
            _op.JSClick(_seeMore);
            List<string> specs = new List<string>();
            foreach (var spec in _specs)
            {
                specs.Add(spec.Text);
            }
            string str = String.Join(" - ", specs);
            return specs;
        }

        public string GetUPC()
        {
            _op.JSClick(_seeMore);
            string upc = _upc.Text;
            var upcValue = upc.Split(": ", StringSplitOptions.None).Last();
            return upcValue;
        }

        public ImagesPage OpenAllImagesPage()
        {
            _op.JSClick(_seeMoreImages);
            return new ImagesPage(_driver);
        }
    }
}
