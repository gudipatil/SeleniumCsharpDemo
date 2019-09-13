﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharpDemo.Utilities
{
    class Wait
    {
        public static IWebElement ElementIsVisible(IWebDriver driver, string value, int seconds = 5, string key = "XPath")
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, seconds));
            if (key == "XPath")
            {
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(value)));
                return element;
            }
            if (key == "ID")
            {
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(value)));
                return element;
            }
            if (key == "ClassName")
            {
                IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName(value)));
                return element;
            }

            return null;


        }

    }
}
