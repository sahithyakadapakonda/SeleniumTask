using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;


namespace SeleniumTask
{
    class SetMethods
    {
        

        //Reading Text File
        public string ReadTextFile()
        {

            String line = "";
                StreamReader sr = new StreamReader(@"C:\Users\kaush\OneDrive\Desktop\Demo.txt");
                while (line != null)
                {
                    line = sr.ReadLine();
                    return line;
                }
                sr.Close();
                Console.ReadLine();
          
            return line;
        }

          

        public static string Capture(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "ExtentReports\\" + screenShotName + ".png";
            string localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return localpath;
        }

        public static void EnterText(IWebDriver driver, String element, string value, string elementtype)
        {
            if (elementtype == "id")
                driver.FindElement(By.Id(element)).SendKeys(value);
            if (elementtype == "Name")
                driver.FindElement(By.Id(element)).SendKeys(value);
        }
        public static void Click(IWebDriver driver, String element, string elementtype)
        {
            if (elementtype == "id")
                driver.FindElement(By.Id(element)).Click();
            if (elementtype == "Name")
                driver.FindElement(By.Id(element)).Click();
        }

        public static void SelectDropdown(IWebDriver driver, String element, string value, string elementtype)
        {
            if (elementtype == "id")
                new SelectElement(driver.FindElement(By.Id(element))).SelectByText(value);
            if (elementtype == "Name")
                new SelectElement(driver.FindElement(By.Name(element))).SelectByText(value);
        }

        
    }
}
