using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumCsharpDemo.Pages
{
    public class HomePage
    {

        [FindsBy(How = How.XPath, Using = "//a[contains(.,'Hello hari!')]")]
        private IWebElement message { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(.,'Administration')]")]
        private IWebElement adminbtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(.,'Time & Materials')]")]
        private IWebElement timeandmaterial { get; set; }

        internal void VerifyHomePage()
        {
            // Check whether hello hari is displayed on the page
            Assert.IsTrue(message.Text == "Hello hari!");
        }

        internal void ClickAdminstration()
        {
            // Click adminstration 
            adminbtn.Click();
        }

        internal void ClickTimenMaterial()
        {
            // Click Time n Material 
            timeandmaterial.Click();
        }
    }
}
