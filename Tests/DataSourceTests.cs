using System;
using System.Collections.Generic;
using CCGTechTest.DataManagement.Converters;
using CCGTechTest.DataManagement.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DataSourceTests
    {
        [TestMethod]
        public void TestCSVDataSource_Positive()
        {
            var source = new CSVDataSource();

            source.Load();

            var expected = new List<string>()
            {
                "name,address_line1,address_line2",
                "Dave,Market Street,London",
                "John,Queen Street,York"
            };

            var actual = source.Data;

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
