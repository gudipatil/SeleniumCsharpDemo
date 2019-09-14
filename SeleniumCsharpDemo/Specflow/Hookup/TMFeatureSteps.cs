using System;
using TechTalk.SpecFlow;
using SeleniumCsharpDemo;
using SeleniumCsharpDemo.Pages;
using OpenQA.Selenium.Chrome;
using SeleniumCsharpDemo.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Configuration;
using SeleniumCsharpDemo.WrapperFactory;

namespace SeleniumCsharpDemo.Specflow.Hookup
{
    [Binding]
    public class TMFeatureSteps
    {
        
        [Given(@"I have logged into the turn up portal")]
        public void GivenIHaveLoggedIntoTheTurnUpPortal()
        {
            BrowserFactory.InitBrowser("Chrome");
            BrowserFactory.LoadApplication(ConfigurationManager.AppSettings["URL"]);

            // login into Turn up portal
            Page.Login.Login();
        }

        [Given(@"I have navigate to the time and material page")]
        public void GivenIHaveNavigateToTheTimeAndMaterialPage()
        {
            Page.Home.VerifyHomePage();
            Page.Home.ClickAdminstration();
            Page.Home.ClickTimenMaterial();
        }

        [Then(@"I should be able to create a time and material record\.")]
        public void ThenIShouldBeAbleToCreateATimeAndMaterialRecord_()
        {
            ExcelReader.PopulateInCollection("Create");
            string typecode = Utilities.ExcelReader.ReadData(2, "TypeCode");
            string code = ExcelReader.ReadData(2, "Code");
            string description = ExcelReader.ReadData(2, "Description");
            string price = ExcelReader.ReadData(2, "PricePerUnit");

            Page.TimeAndMaterial.ClickCreateNew();
            Page.TimeAndMaterial.EnterValidDataandSave(typecode, code, description, price);
            Assert.IsTrue("RecordFound" == Page.TimeAndMaterial.ValidateData(typecode, code, description, "$" + price), "Created record not found");
            BrowserFactory.CloseDriver();
        }

        [Then(@"I should be able to delete a time and material record\.")]
        public void ThenIShouldBeAbleToDeleteATimeAndMaterialRecord_()
        {
            // Reads data from the excel file with sheetname "Delete"
            ExcelReader.PopulateInCollection("Delete");
            string typecode = ExcelReader.ReadData(2, "TypeCode");
            string code = ExcelReader.ReadData(2, "Code");
            string description = ExcelReader.ReadData(2, "Description");
            string price = ExcelReader.ReadData(2, "PricePerUnit");

            Page.TimeAndMaterial.ClickCreateNew();
            Page.TimeAndMaterial.EnterValidDataandSave(typecode, code, description, price);
            Page.TimeAndMaterial.DeleteData(typecode, code, description, "$" + price);

            // Verifies if the record is deleted
            Page.Home.ClickAdminstration();
            Page.Home.ClickTimenMaterial();
            Assert.IsTrue("RecordNotFound" == Page.TimeAndMaterial.ValidateData(typecode, code, description, "$" + price), "Validate Failed");

            BrowserFactory.CloseDriver();
        }

        [Then(@"I should be able to edit a time and material record\.")]
        public void ThenIShouldBeAbleToEditATimeAndMaterialRecord_()
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
            BrowserFactory.CloseDriver();


        }
    }
}
