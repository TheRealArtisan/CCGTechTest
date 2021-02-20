using CCGTechTest.DataManagement.Converters;
using CCGTechTest.DataManagement.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CCGTechTest.DataManagement
{
    public static class Data
    {
        static Dictionary<string, DataSource> sourceMapping;

        static Dictionary<string, DataConverter> outputMapping;

        public static T GetData<T>(string sourceTag, string outputTag, string sourcePath = null, params object[] args)
        {
            MapSources();
            MapOutputs();

            DataSource source = null;
            DataConverter converter = null;

            if (sourceMapping.ContainsKey(sourceTag))
            {
                source = sourceMapping[sourceTag];
            }
            else
            {
                throw new KeyNotFoundException($"Could not find {nameof(DataSource)} with tag {sourceTag}.");
            }

            if (outputMapping.ContainsKey(outputTag))
            {
                if (source != null)
                {
                    converter = outputMapping[outputTag];
                }
            }
            else
            {
                throw new KeyNotFoundException($"Could not find {nameof(DataConverter)} with tag {outputTag}.");
            }

            source.Load();
            
            return converter.Serialize<T>(source.Data, args);
        }

        private static void MapSources()
        {
            if (sourceMapping == null)
            {
                sourceMapping = new Dictionary<string, DataSource>();

                var types = Assembly.GetCallingAssembly().GetTypes();

                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof(DataSource)))
                    {
                        var ctor = type.GetConstructor(Type.EmptyTypes);

                        if (ctor != null)
                        {
                            var item = ctor.Invoke(null) as DataSource;

                            if (item != null)
                            {
                                if (sourceMapping.ContainsKey(item.Tag))
                                {
                                    throw new Exception($"{nameof(sourceMapping)} already contains key/tag {item.Tag}.");
                                }
                                sourceMapping.Add(item.Tag, item);
                            }
                        }
                    }
                }
            }
        }

        private static void MapOutputs()
        {
            if (outputMapping == null)
            {
                outputMapping = new Dictionary<string, DataConverter>();

                var types = Assembly.GetCallingAssembly().GetTypes();

                foreach (var type in types)
                {
                    if (type.IsSubclassOf(typeof(DataConverter)))
                    {
                        var ctor = type.GetConstructor(Type.EmptyTypes);

                        if (ctor != null)
                        {
                            var item = ctor.Invoke(null) as DataConverter;

                            if (item != null)
                            {
                                if (outputMapping.ContainsKey(item.Tag))
                                {
                                    throw new Exception($"{nameof(outputMapping)} already contains key/tag {item.Tag}.");
                                }
                                outputMapping.Add(item.Tag, item);
                            }
                        }
                    }
                }
            }
        }
    }
}
