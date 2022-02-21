using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class JsonObject : DataObject
    {
        private readonly Dictionary<string, JsonValue> items;
        private new readonly DataNode node;
        public JsonObject()
        {
            items = new Dictionary<string, JsonValue>();
            node = new DataNode();
        }

        public JsonObject(DataNode parent) : this()
        {
            node = new DataNode(parent);
        }

        public override void Add(string key, dynamic? value, int line)
        {
            items.Add(key, new JsonValue(value, line));
        }

        public override string ToString()
        {
            return this.ToString("");
        }

        public override string ToString(string tabs)
        {
            StringBuilder sb = new();
            sb.Append("{\n");
            for (int i = 0; i < items.Count; i++)
            {
                KeyValuePair<string, JsonValue> item = items.ElementAt(i);
                sb.Append(tabs);
                sb.Append("\t\"");
                sb.Append(item.Key);
                sb.Append("\": ");
                sb.Append(item.Value.ToString(tabs));
                if (i < items.Count - 1)
                    sb.Append(',');
                sb.Append('\n');
            }
            sb.Append(tabs);
            sb.Append('}');
            return sb.ToString();
        }

        public override DataValue Query(string search)
        {
            foreach (KeyValuePair<string, JsonValue> item in items)
            {
                if (item.Key == search)
                    return item.Value;
                JsonValue value = item.Value;
                if ((JsonType)value.GetType() == JsonType.Object || (JsonType)value.GetType() == JsonType.Array)
                {
                    JsonValue result = value.GetValue().Query(search);
                    if ((JsonType)result.GetType() != JsonType.Empty)
                        return result;
                }
            }
            return new JsonValue();
        }
    }
}
