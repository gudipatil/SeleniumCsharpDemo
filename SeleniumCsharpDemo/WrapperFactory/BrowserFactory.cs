using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharpDemo.WrapperFactory
{
    class BrowserFactory
    {
        private static IWebDriver driver;

        public static IWebDriver Driver
        {
            get
            {
                if (driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                return driver;
            }
            private set
            {
                driver = value;
            }
        }

        public static void InitBrowser(string browserName)
        {
            switch (browserName.ToUpper())
            {
                case "FIREFOX":
                    if (driver == null)
                    {
                        driver = new FirefoxDriver();
                    }
                    break;
                case "CHROME":
                    if (driver == null)
                    {
                        driver = new ChromeDriver();
                    }
                    break;
                default:
                    Console.WriteLine("Supports firefox and chrome browser");
                    break;
            }
        }
        public static void LoadApplication(string url)
        {
            Driver.Navigate().GoToUrl(url);
            Driver.Manage().Window.Maximize();
        }

        public static void CloseDriver()
        {
            Driver.Close();
            Driver.Quit();
            // To-do  Make it thread safe to run parallel tests
            // Don't want to create a instance of BrowserFactory(), for this reason driver should be set to null
            // after a Close()/Quit().
            driver = null;
                          
        }
    }

}
