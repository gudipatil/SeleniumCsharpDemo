using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharpDemo.Utilities
{
    class JSExecutor
    {
        public static object JavaScriptExec(IWebDriver driver, string cmd)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(cmd);
        }
    }
}
