using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharpDemo.Utilities
{
    class ExcelReader
    {
        public static void PopulateInCollection(string sheetname, string filename = null)
        {
            if (filename == null)
            {
                Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
                filename = AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug\", @"TestData\TurnUpPortalTestData.xlsx");
            }
            ExcelLib.PopulateInCollection(filename, sheetname);
        }

        public static string ReadData(int rownumber, string colname)
        {
            return ExcelLib.ReadData(rownumber, colname);
        }
    }
}
