using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCGTechTest.DataManagement.DataSources
{
    public class CSVDataSource : DataSource
    {
        public override string Tag => "csv";

        public override string Name => "CSV Data Source";

        public override string Description => $"Some description about the {nameof(CSVDataSource)}.";

        /// <summary>
        /// Loads the data from the source.
        /// "Args[0] : String sourcePath"
        /// </summary>
        /// <param name="args"></param>
        public override void Load(params object[] args)
        {
            string sourcePath = "";

            if (args != null && args.Length > 0)
            {
                if (args[0] != null)
                {
                    if (args[0].GetType() == typeof(string))
                    {
                        sourcePath = (string)args[0];
                    }
                }
            }

            var lines = new List<string>();

            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                lines = Resources.Resources.ExampleCSV.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else
            {
                if (File.Exists(sourcePath))
                {
                    lines = File.ReadAllLines(sourcePath).ToList();
                }
                else
                {
                    throw new FileNotFoundException($"Could not find file '{sourcePath}'.");
                }
            }

            Data = lines;
        }
    }
}
