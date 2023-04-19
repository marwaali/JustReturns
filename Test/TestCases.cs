using JustReturns.Pages;
using JustReturns.Test;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using Task.Pages;
using Task.Utlities;

namespace Task
{

    [TestFixture]
    public class TestCases: BaseTest
    {
        [Test]
        public void GetProductData()
        { 
            foreach (var item in upc)
            {
                GooglePage google = new GooglePage(driver);
                google.SearchForProduct(item);
                google.ClickOnLink("https://www.target.com");

                ItemDetailsPage details = new ItemDetailsPage(driver);

                List<string> specs = details.GetSpecs();
                //string description = details.GetDescription();
                //List<string> highlights = details.GetHighlights();
                

                //string data = upc + "," + description + "," + highlights + "," + specs + "\r";
                //FileHundler.AppendCSVData(data, csvfile);
                DirectoryInfo di = Directory.CreateDirectory(projDir + @"\Data\" + item);
                //FileHundler.WriteDataToNotePade(di.FullName, item, description, highlights, specs);
                FileHundler.WriteDataToNotePade(di.FullName, item, specs);

                //ImagesPage images = details.OpenAllImagesPage();
                //di = Directory.CreateDirectory(projDir + @"\Data\" + item + @"\Images\");
                //images.GetImagesLink(di.FullName);
                driver.Navigate().GoToUrl(dl.website[0].url);
            }
            
        }
    }
}