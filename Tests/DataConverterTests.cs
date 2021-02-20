using System;
using System.Collections.Generic;
using CCGTechTest.DataManagement;
using CCGTechTest.DataManagement.Converters;
using CCGTechTest.DataManagement.DataSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DataConverterTests
    {
        [TestMethod]
        public void TestGettingFormatOptions_Positive()
        {
            var conv = new JsonConverter();

            var actual = conv.GetFormatOptions(FormatOptions.Indented);

            var expected = FormatOptions.Indented;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGettingFormatOptions_Negative()
        {
            var conv = new JsonConverter();

            var actual = conv.GetFormatOptions(FormatOptions.Indented);

            var expected = FormatOptions.None;

            Assert.AreNotEqual(expected, actual);
        }
        
        [TestMethod]
        public void TestSerialization_WithoutGroupHeadings_Positive()
        {
            string expected = Resources.TestJsonExample_WithoutGroupHeadings.ToString();

            var data = new List<string>()
            {
                "name,address_line1,address_line2",
                "Dave,Market Street,London",
                "John,Queen Street,York"
            };

            string actual = new JsonConverter().Serialize<string>(data, FormatOptions.None);

            //Remove any formating characters
            expected = new JsonConverter().RemoveFormatting(expected);
            actual = new JsonConverter().RemoveFormatting(actual);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSerialization_WithoutGroupHeadings_Negative()
        {
            string expected = Resources.TestJsonExample_WithoutGroupHeadings.ToString();

            //added an extra 'space' to the start of 'Queen Street'
            var data = new List<string>()
            {
                "name,address_line1,address_line2",
                "Dave,Market Street,London",
                "John, Queen Street,York"
            };

            string actual = new JsonConverter().Serialize<string>(data, FormatOptions.None);

            //Remove any formating characters
            expected = new JsonConverter().RemoveFormatting(expected);
            actual = new JsonConverter().RemoveFormatting(actual);

            Assert.AreNotEqual(expected, actual);
        }

    }
}
