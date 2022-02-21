using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class JsonArray : DataObject
    {
        private readonly List<JsonValue> values;
        private JsonType type;
        private new readonly DataNode node;
        public JsonArray()
        {
            values = new List<JsonValue>();
            type = JsonType.Null;
            node = new DataNode();
        }

        public JsonArray(DataNode parent) : this()
        {
            node = new DataNode(parent);
        }

        public override void Add(string key, dynamic? value, int line)
        {
            Add(value, line);
        }

        public void Add(dynamic? value, int line)
        {
            JsonValue item = new(value, line);
            if (values.Count > 0)
            {
                if (type != (JsonType)item.GetType())
                    throw new DataException(line);
            }
            else
            {
                type = (JsonType)item.GetType();
            }
            values.Add(item);

        }

        public override string ToString()
        {
            return this.ToString("");
        }

        public override string ToString(string tabs)
        {
            StringBuilder sb = new();
            sb.Append("[\n");
            for (int i = 0; i < values.Count; i++)
            {
                JsonValue item = values[i];
                sb.Append(tabs);
                sb.Append('\t');
                sb.Append(item.ToString(tabs));
                if (i < values.Count - 1)
                    sb.Append(',');
                sb.Append('\n');
            }
            sb.Append(tabs);
            sb.Append(']');
            return sb.ToString();
        }

        public override DataValue Query(string search)
        {
            if (type != JsonType.Object && type != JsonType.Array)
                return new JsonValue();
            foreach (JsonValue item in values)
            {
                JsonValue result = item.GetValue().Query(search);
                if ((JsonType)result.GetType() != JsonType.Empty)
                    return result;
            }
            return new JsonValue();
        }
    }
}
