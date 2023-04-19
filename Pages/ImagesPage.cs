using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Task.Pages
{
    public class ImagesPage
    {
        private IWebDriver _driver;
        private IList<IWebElement> _images;
        private IList<IWebElement> _videos;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="driver"> webdriver </param>
        public ImagesPage(IWebDriver driver)
        {
            _driver = driver;
            _images = _driver.FindElements(By.CssSelector("nav[class*='CarouselProductModalThumbnailWrapper'] img"));
            
        }

        public void GetImagesLink(string path)
        {
            string url = string.Empty;
            List<string> imgs = new List<string>();
            List<string> videos = new List<string>();
            WebClient downloader = new WebClient();
            foreach (var img in _images)
            {
                url = img.GetAttribute("src").Split("?", StringSplitOptions.None).First();
                imgs.Add(url);
            }

            for (int i = 0; i <imgs.Count; i++)
            {
                if(imgs[i].StartsWith("https://"))
                {
                    downloader.DownloadFile(imgs[i], path + "\\" + i + ".jpg");
                }

                else
                {

                    try
                    {
                        _videos = _driver.FindElements(By.CssSelector("nav[class*='CarouselProductModalThumbnailWrapper'] button[aria-label*=video]"));
                        foreach (var video in _videos)
                        {
                            video.Click();
                            IWebElement videoElement = _driver.FindElement(By.CssSelector("div[class*='CarouselProductVideoWrapper-sc-cwwbs3-5'] video source"));
                            downloader.DownloadFile(videoElement.GetAttribute("src"), path + "\\" + i + ".mp4");
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("There is an issue");
                    }
                }
            }
        }
    }
}