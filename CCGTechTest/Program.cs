using CCGTechTest.DataManagement;
using CCGTechTest.DataManagement.Converters;
using CCGTechTest.DataManagement.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCGTechTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //string csvExamplePath = @"C:\Users\Dave Hutchinson\Documents\Projects\CCGTechTest\CCGTechTest\Resources\ExampleCSV.csv";

                Console.WriteLine(Data.GetData<string>("csv", "json", null, FormatOptions.Indented));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
