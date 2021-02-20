using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCGTechTest.DataManagement.DataSources
{
    public abstract class DataSource
    {
        public abstract string Tag { get; }

        public abstract string Name { get; }

        public abstract string Description { get; }
        
        public List<string> Data { get; protected set; }

        public DataSource()
        {
            
        }

        public abstract void Load(params object[] args);
    }
}
