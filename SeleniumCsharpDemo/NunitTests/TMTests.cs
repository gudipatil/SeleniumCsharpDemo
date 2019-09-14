using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumCsharpDemo.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using SeleniumCsharpDemo.Utilities;
using SeleniumCsharpDemo.WrapperFactory;
using System.Configuration;

namespace SeleniumCsharpDemo.NunitTests
{
    [TestFixture]
    class TMTests
    {

        [SetUp]
        public void BeforeEachTestCase()
        {
            BrowserFactory.InitBrowser("Chrome");
            BrowserFactory.LoadApplication(ConfigurationManager.AppSettings["URL"]);
                              
            Page.Login.Login();
        }

        [TearDown]
        public void AfterEachTestCase()
        {
            // Close driver
            BrowserFactory.CloseDriver();
        }

        [Test]
        public void CreateTMnValidate()
        {
            ExcelReader.PopulateInCollection("Create");
            string typecode = ExcelReader.ReadData(2, "TypeCode");
            string code = ExcelReader.ReadData(2, "Code");
            string description = ExcelReader.ReadData(2, "Description");
            string price = ExcelReader.ReadData(2, "PricePerUnit");

            Page.Home.VerifyHomePage();
            Page.Home.ClickAdminstration();
            Page.Home.ClickTimenMaterial();

            Page.TimeAndMaterial.ClickCreateNew();
            Page.TimeAndMaterial.EnterValidDataandSave(typecode, code, description, price);
            Assert.IsTrue("RecordFound" == Page.TimeAndMaterial.ValidateData(typecode, code, description, "$" + price), "Created record not found");
       
        }

        [Test]
        public void EditnValidate()
        {
            Page.Home.VerifyHomePage();
            Page.Home.ClickAdminstration();
            Page.Home.ClickTimenMaterial(); 

            // Convert excel data in "Edit" sheet into tables
            ExcelReader.PopulateInCollection("Edit");
            string typecode = ExcelReader.ReadData(2, "TypeCode");
            string code = ExcelReader.ReadData(2, "Code");
            string description = ExcelReader.ReadData(2, "Description");
            string price = ExcelReader.ReadData(2, "PricePerUnit");
            string newtypecode = ExcelReader.ReadData(2, "NewTypeCode");
            string newcode = ExcelReader.ReadData(2, "NewCode");
            string newdescription = ExcelReader.ReadData(2, "NewDescription");
            string newprice = ExcelReader.ReadData(2, "NewPrice");

            Console.WriteLine("typecode: " + typecode + "\n" + "Code:" + code + "\n" + "Desc:" + description + "\n" + "Price:" + price + "\n" + "newtypecode:" + newtypecode + "\n" + "newdesc:" + newdescription + "\n" + "newprice:" + newprice + "\n");

            string result = Page.TimeAndMaterial.EditValidDataandSave(typecode, code, description, "$" + price, newtypecode, newcode, newdescription, newprice);
            Assert.IsTrue("success" == result, "Edit failed");
            Assert.IsTrue("RecordFound" == Page.TimeAndMaterial.ValidateData(newtypecode, newcode, newdescription, "$" + newprice), "Validate failed");
            Page.Home.ClickAdminstration();
            Page.Home.ClickTimenMaterial();
            Page.TimeAndMaterial.DeleteData(newtypecode, newcode, newdescription, "$" + newprice);
            
        }

        [Test]
        public void DeletenValidate()
        {
            // Reads data from the excel file with sheetname "Delete"
            ExcelReader.PopulateInCollection("Delete");
            string typecode = ExcelReader.ReadData(2, "TypeCode");
            string code = ExcelReader.ReadData(2, "Code");
            string description = ExcelReader.ReadData(2, "Description");
            string price = ExcelReader.ReadData(2, "PricePerUnit");

            // Verifies if the record is deleted
            Page.Home.ClickAdminstration();
            Page.Home.ClickTimenMaterial();

            Page.TimeAndMaterial.ClickCreateNew();
            Page.TimeAndMaterial.EnterValidDataandSave(typecode, code, description, price);
            Page.TimeAndMaterial.DeleteData(typecode, code, description, "$" + price);

            // Verifies if the record is deleted
            Page.Home.ClickAdminstration();
            Page.Home.ClickTimenMaterial();
            Assert.IsTrue("RecordNotFound" == Page.TimeAndMaterial.ValidateData(typecode, code, description, "$" + price), "Validate Failed");
        }
    }
}
