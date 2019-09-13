using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumCsharpDemo.Utilities;
using SeleniumCsharpDemo.WrapperFactory;
using SeleniumExtras.PageObjects;

namespace SeleniumCsharpDemo.Pages
{
    public class TimeAndMaterialPage
    {

        [FindsBy(How = How.XPath, Using = "//a[contains(.,'Create New')]")]
        private IWebElement createnew { get; set; }

        [FindsBy(How = How.Id, Using = "Code")]
        private IWebElement codedrpdwn { get; set; }

        [FindsBy(How = How.Id, Using = "Description")]
        private IWebElement desctxtbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[contains(@class,'k-formatted-value k-input')]")]
        private IWebElement pricetxtbox { get; set; }

        [FindsBy(How = How.Id, Using = "SaveButton")]
        private IWebElement savebtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"TimeMaterialEditForm\"]/div/div[4]/div/span[1]/span/input[1]")]
        private IWebElement Price { get; set; }

        [FindsBy(How = How.Id, Using = "Price")]
        private IWebElement Price1 { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(.,'Go to the next page')]")]
        private IWebElement nextpage { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"tmsGrid\"]/div[4]/a[3]")]
        private IWebElement lastpage { get; set; }

        [FindsBy(How = How.Id, Using = "TypeCode_listbox")]
        private IWebElement typecodelistbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"TimeMaterialEditForm\"]/div/div[1]/div/span[1]/span/span[1]")]
        private IWebElement dropdown { get; set; }

        internal void ClickCreateNew()
        {
            // Click create new button
            createnew.Click();
        }

        internal void EnterValidDataandSave(string typecode, string code, string description, string price)
        {
            // Select TypeCode from dropdown
            SelectTypeCode(typecode);

            // Enter code 
            codedrpdwn.SendKeys(code);

            // Enter description
            desctxtbox.SendKeys(description);

            // Enter price
            pricetxtbox.SendKeys(price);

            // Click save
            savebtn.Click();
        }

        internal string ValidateData(string typecode, string code, string desc, string price)
        {
            // Checks if the time and material record exists
            int result = SearchRecord(typecode, code, desc, price);
            if (result > 0) { return "RecordFound"; }
            return "RecordNotFound";
        }

        internal string DeleteData(string typecode, string code, string desc, string price, string alertkey = "ok")
        {
            // Finds and delete the given employee record
            int colnumber = SearchRecord(typecode, code, desc, price);
            if (colnumber < 1) { return "RecordNotFound"; }

            // Click delete button
            string deletebtn = String.Format("((//a[@class='k-button k-button-icontext k-grid-Delete'][contains(.,'Delete')])[{0}])", colnumber);
            BrowserFactory.Driver.FindElement(By.XPath(deletebtn)).Click();

            if (alertkey == "ok")
            {
                BrowserFactory.Driver.SwitchTo().Alert().Accept();
                Console.WriteLine("record deleted");
            }
            else if (alertkey == "cancel")
            {
                BrowserFactory.Driver.SwitchTo().Alert().Dismiss();
            }
            return "success";
        }

        internal string EditValidDataandSave(string typecode, string code, string desc, string price, string newtypecode, string newcode, string newdesc, string newprice)
        {
            int colnumber = SearchRecord(typecode, code, desc, price);
            if (colnumber < 0) { return "RecordNotFound"; }

            // Click edit button
            string editbtn = String.Format("(//a[@class='k-button k-button-icontext k-grid-Edit'][contains(.,'Edit')])[{0}]", colnumber);
            BrowserFactory.Driver.FindElement(By.XPath(editbtn)).Click();

            // Select Time/Material TypeCode dropdown
            SelectTypeCode(newtypecode);

            //Enter code
            if (newcode != null)
            {
                codedrpdwn.Clear();
                codedrpdwn.SendKeys(newcode);
            }

            // Enter description
            if (newdesc != null)
            {
                desctxtbox.Clear();
                desctxtbox.SendKeys(newdesc);
            }

            // Enter Price
            if (newprice != null)
            {
                Price.Clear();         
                Price1.Clear();
                Price.SendKeys(newprice);
            }

            // click save
            savebtn.Click();
            return "success";
        }

        internal void SelectTypeCode(string typecode)
        {
            // Selects type code to Time or Material in a drop down list
            dropdown.Click();
            var options = typecodelistbox.FindElements(By.TagName("li"));
            if (typecode != null)
            {
                foreach (var opt in options)
                {
                    if (opt.Text == typecode)
                    {
                        opt.Click();
                        break;
                    }
                };
            }
        }
        internal string TypeCodeInTable(string typecode)
        {
            // Typecode in table is set to M/T
            if (typecode == "Material" || typecode == "M" || typecode == null) { typecode = "M"; }
            if (typecode == "Time" || typecode == "T") { typecode = "T"; }
            return typecode;
        }

        internal int SearchRecord(string typecode, string code, string desc, string price)
        {
            try
            {
                IWebElement table = Wait.ElementIsVisible(BrowserFactory.Driver, "//*[@id=\"tmsGrid\"]/div[3]/table", 10);
                typecode = TypeCodeInTable(typecode);
                while (true)
                {
                    var rows = table.FindElements(By.TagName("tr"));
                    int colnumber = 1;
                    foreach (var row in rows)
                    {
                        var columns = row.FindElements(By.TagName("td")).ToList();
                        if ((columns[0].Text == code) && (columns[1].Text == typecode) && (columns[2].Text == desc) && (columns[3].Text == price))
                        { return colnumber; }
                        colnumber++;
                    }
                    // Navigate to next page until the next button is disabled, otherwise the records in last page are read again and again
                    if (!lastpage.GetAttribute("class").Contains("k-state-disabled"))
                    {
                        nextpage.Click();
                    }
                    else
                    {
                        return -1;
                    }

                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return -1;
        }

    }
}
