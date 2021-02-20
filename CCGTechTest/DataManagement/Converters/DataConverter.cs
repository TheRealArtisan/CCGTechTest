using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCGTechTest.DataManagement.Converters
{
    public abstract class DataConverter
    {
        public abstract string Tag { get; }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public List<string> Data { get; protected set; }

        public DataConverter()
        {

        }

        public abstract T Serialize<T>(List<string>data, params object[] args);
    }
}
