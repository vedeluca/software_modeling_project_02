using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing
{
    public class JsonObject
    {
        private Dictionary<string, JsonValue> Dict;
        public JsonObject()
        {
            Dict = new Dictionary<string, JsonValue>();
        }

        public void Add(string key, dynamic? value, int line)
        {
            Dict.Add(key, new JsonValue(value, line));
        }

        new public string ToString()
        {
            return this.ToString("");
        }

        public string ToString(string tabs)
        {
            StringBuilder sb = new();
            sb.Append("{\n");
            for (int i = 0; i < Dict.Count; i++)
            {
                KeyValuePair<string, JsonValue> item = Dict.ElementAt(i);
                sb.Append(tabs);
                sb.Append("\t\"");
                sb.Append(item.Key);
                sb.Append("\": ");
                sb.Append(item.Value.ToString(tabs));
                if (i < Dict.Count - 1)
                    sb.Append(',');
                sb.Append('\n');
            }
            sb.Append(tabs);
            sb.Append('}');
            return sb.ToString();
        }

        public JsonValue? Query(string search)
        {
            foreach (KeyValuePair<string, JsonValue> item in Dict)
            {
                if (item.Key == search)
                    return item.Value;
                dynamic? value = item.Value.GetValue();
                if (value is JsonObject || value is JsonArray)
                {
                    dynamic? queryResult = value.Query(search);
                    if (queryResult != null)
                        return queryResult;
                }
            }
            return null;
        }
    }
}
