using CCGTechTest.DataManagement.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCGTechTest.DataManagement.Converters
{
    public class JsonConverter : DataConverter
    {
        public override string Tag => "json";

        public override string Name => "Json Converter";

        public override string Description => $"Some description about the {nameof(JsonConverter)}.";

        /// <summary>
        /// Serializes the data to Json format.
        /// "T : Expected type of String.",
        /// "Args[0] : FormatOption options"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override T Serialize<T>(List<string> data, params object[] args)
        {
            if (typeof(T) != typeof(string))
            {
                throw new Exception($"Serialization error. Unexpected type {typeof(T).Name}. Expected type String.");
            }

            if (data != null)
            {
                Data = data;
            }

            var options = GetFormatOptions(args);

            string json = BuildJson(options);

            return (T)Convert.ChangeType(json, typeof(T));
        }
        
        private string BuildJson(FormatOptions options = FormatOptions.None)
        {
            var jsonBuilder = new StringBuilder();

            jsonBuilder.AppendLine("{");

            jsonBuilder.AppendLine("\"data\": [");

            var jsonDataItems = new List<string>();

            foreach (var line in Data.Skip(1))
            {
                jsonDataItems.Add(BuildDataItemJsonFromLine(Data[0], line));
            }

            jsonBuilder.AppendLine(string.Join(",", jsonDataItems));

            jsonBuilder.AppendLine("]");

            jsonBuilder.AppendLine("}");

            string formatted = Format(jsonBuilder.ToString(), options);

            return formatted;
        }

        private string BuildDataItemJsonFromLine(string headers, string line)
        {
            var headerList = headers.Split(',');
            var paramList = line.Split(',');

            var dataItemBuilder = new StringBuilder();

            dataItemBuilder.AppendLine("{");

            var jsonParams = new List<string>();

            for (int i = 0; i < headerList.Length; i++)
            {
                string header = headerList[i];
                string param = paramList[i];

                string json = $"\"{header}\" : \"{param}\"";

                jsonParams.Add(json);
            }

            dataItemBuilder.AppendLine(string.Join(",", jsonParams));

            dataItemBuilder.AppendLine("}");

            return dataItemBuilder.ToString();
        }

        public FormatOptions GetFormatOptions(params object[] args)
        {
            var options = FormatOptions.None;

            if (args != null && args.Length > 0)
            {
                if (args[0] != null)
                {
                    if (args[0].GetType() == typeof(FormatOptions))
                    {
                        if (FormatOptions.Indented == (FormatOptions)args[0])
                        {
                            options = FormatOptions.Indented;
                        }
                    }
                }
            }

            return options;
        }

        private string Format(string original, FormatOptions options)
        {
            switch (options)
            {
                case FormatOptions.None:
                    return RemoveFormatting(original);
                case FormatOptions.Indented:
                    return FormatIndented(original);
                default:
                    throw new Exception($"Unxepted format option {options.ToString()}.");
            }
        }

        public string RemoveFormatting(string original)
        {
            string formatted = "";

            Newtonsoft.Json.Linq.JToken token = Newtonsoft.Json.Linq.JToken.Parse(original);

            formatted = token.ToString(Newtonsoft.Json.Formatting.None);

            return formatted;
        }

        private string FormatIndented(string original)
        {
            string formatted = "";

            Newtonsoft.Json.Linq.JToken token = Newtonsoft.Json.Linq.JToken.Parse(original);

            formatted = token.ToString(Newtonsoft.Json.Formatting.Indented);

            return formatted;
        }
    }

    public enum FormatOptions
    {
        None,
        Indented
    }
}
