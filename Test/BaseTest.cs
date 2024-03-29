﻿using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Task;
using Task.Utlities;

namespace JustReturns.Test
{
    public class BaseTest
    {
        protected ExtentReports _extent;
        protected ExtentTest _test;
        public string projDir;
        public DataList dl;
        string browser;
        public List<string> upc;
        public IWebDriver driver;



        [OneTimeSetUp]
        public void BeforeClass()
        {
            projDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = projDir + @"\Data.xml";
            XmlUtlities method = new XmlUtlities();
            dl = method.Deserialize(path);
            browser = dl.website[0].browser;
            upc = dl.UPCs[0].UPC;


            try
            {
                _extent = new ExtentReports();
                DirectoryInfo di = Directory.CreateDirectory(projDir + "\\Test_Execution_Reports");
                var htmlReporter = new ExtentHtmlReporter(di + "\\Automation_Report" + ".html");
                _extent.AddSystemInfo("Environment", "Technical Test");
                _extent.AddSystemInfo("User Name", "Marwa");
                _extent.AttachReporter(htmlReporter);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [SetUp]
        public void Setup()
        {
            try
            {

                switch (browser)
                {
                    case "chrome":
                        driver = new ChromeDriver();
                        break;
                    case "firefox":
                        driver = new FirefoxDriver();
                        break;
                    default:
                        break;
                }
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
                _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
                driver.Navigate().GoToUrl(dl.website[0].url);
            }
            catch (Exception e)
            {
                throw (e);
            }

        }

        [TearDown]
        public void AfterTest()
        {
            DriverOperations op = new DriverOperations(driver);
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
                var errorMessage = TestContext.CurrentContext.Result.Message;
                Status logstatus;
                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        string screenShotPath = Capture(driver, TestContext.CurrentContext.Test.Name);
                        _test.Log(logstatus, "Test ended with " + logstatus + " – " + errorMessage);
                        _test.Log(logstatus, "Snapshot below: " + _test.AddScreenCaptureFromPath(screenShotPath));
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        _test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                    default:
                        logstatus = Status.Pass;
                        _test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            op.CloseDriver();
        }

        [OneTimeTearDown]
        public void AfterClass()
        {
            try
            {
                _extent.Flush();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        private string Capture(IWebDriver driver, string screenShotName)
        {
            string localpath = "";
            try
            {
                Thread.Sleep(4000);
                Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
                DirectoryInfo di = Directory.CreateDirectory(projDir + @"\Defect_Screenshots");
                string finalpth = di.FullName + @"\" + screenShotName + ".png";
                localpath = new Uri(finalpth).LocalPath;
                image.SaveAsFile(localpath);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return localpath;
        }
    }
}
