using OpenQA.Selenium;
using SeleniumCsharpDemo.Utilities;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharpDemo.Pages
{
    public class LoginPage
    {

        [FindsBy(How = How.Id, Using = "UserName")]
        private IWebElement username { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        private IWebElement password { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"loginForm\"]/form/div[3]/input[1]")]
        private IWebElement loginbtn { get; set; }
        

        internal void Login()
        {
            // Reads the login credentials from excel sheet
            ExcelReader.PopulateInCollection("Login");
            string usrname = ExcelReader.ReadData(2, "UserName");
            string pwd = ExcelReader.ReadData(2, "Password");

            // Enter login credentials
            username.SendKeys(usrname);
            password.SendKeys(pwd);

            loginbtn.Click();
        }

    }
}
