using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumTask
{
    public class Tests
    {
        ExtentReports extent;

        [OneTimeSetUp]
        public void ExtentStart()
        {
            extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(@"C:\Users\kaush\source\repos\SeleniumTask\ExtentReports\SeleniumTask.html");
            extent.AttachReporter(htmlReporter);
        }

        [OneTimeTearDown]
        public void ExtentClose()
        {
            extent.Flush();
        }

        [Test]
        public void Test1()
        {
            IWebDriver webdriver = webdriver = new ChromeDriver();
            ExtentTest test = extent.CreateTest("Test1").Info("Test Started");
            SetMethods url = new SetMethods();
            string URL = url.ReadTextFile();

            //Launching the browser
            webdriver.Navigate().GoToUrl("https://www.google.com/");
            webdriver.Manage().Window.Maximize();
            if (webdriver.Url.Contains("google.com"))
            {
                test.Log(Status.Pass, "Browser Lauched", MediaEntityBuilder.CreateScreenCaptureFromPath(SetMethods.Capture(webdriver, "Browser launched")).Build());
            }
            else
            {
                test.Log(Status.Fail, "Browser not Lauched", MediaEntityBuilder.CreateScreenCaptureFromPath(SetMethods.Capture(webdriver, "Browser not launched")).Build());
            }

            //Openning Application using parameters
            webdriver.Navigate().GoToUrl(URL);
            test.Log(Status.Pass, "Application Opened", MediaEntityBuilder.CreateScreenCaptureFromPath(SetMethods.Capture(webdriver, "Application Opened")).Build());
            if (!webdriver.Url.Contains("awesome"))
            {
                test.Log(Status.Fail, "Unable to identify element in the webpage", MediaEntityBuilder.CreateScreenCaptureFromPath(SetMethods.Capture(webdriver, "Unable to identify element in the webpage")).Build());
                webdriver.Quit();
            }


            //Selecting Parameters
            SetMethods.SelectDropdown(webdriver, "add1-awed", "Celery", "Id");
            SetMethods.EnterText(webdriver, "Meal", "Banana", "Name");
            test.Log(Status.Pass, "Selected parameters successfully", MediaEntityBuilder.CreateScreenCaptureFromPath(SetMethods.Capture(webdriver, "Selected Parameters")).Build());

            //Count of links and message boxes
            IList<IWebElement> links = webdriver.FindElements(By.TagName("a"));
            int linksCount = links.Count;
            test.Log(Status.Pass, "The number of links are:" + linksCount);
            IList<IWebElement> TextBoxes = webdriver.FindElements(By.TagName("input"));
            test.Log(Status.Pass, "The number of TextBoxes are:" + TextBoxes.Count);
            IList<IWebElement> DropDowns = webdriver.FindElements(By.TagName("select"));
            test.Log(Status.Pass, "The number of Dropdowns are:" + DropDowns.Count);


            Random rd = new Random();
            int RandamNumber;

            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter(@"C:\Users\kaush\OneDrive\Desktop\Write.txt");

            for (int i = 0, count = 0; i < linksCount; i++)
            {
                RandamNumber = rd.Next(0, linksCount + 1);
                if (RandamNumber != 0)
                {
                    links[RandamNumber].Click();
                    test.Log(Status.Info, "The Link number : " + RandamNumber, MediaEntityBuilder.CreateScreenCaptureFromPath(SetMethods.Capture(webdriver, "Link " + RandamNumber + " Opened")).Build());
                    count += 1;

                    string LinkHeader = webdriver.Url;

                    //Write a line of text
                    sw.WriteLine("The Link number is : " + RandamNumber + " and The header of the page is : " + LinkHeader);


                    webdriver.Navigate().GoToUrl(URL);


                    links = webdriver.FindElements(By.TagName("a"));
                    if (count == 2) { break; }
                }
            }

            //Close the file
            sw.Close();


            webdriver.Quit();
            test.Log(Status.Pass, "Task1 passed,Browser Closed");
           

        }
    }
}