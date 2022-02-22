using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class JsonArray : IDataObject
    {
        private readonly List<DataValue> values;
        private DataType type;
        public JsonArray()
        {
            values = new List<DataValue>();
            type = DataType.Null;
        }

        public void Add(string key, dynamic? value, int line)
        {
            Add(value, line);
        }

        public void Add(dynamic? value, int line)
        {
            DataValue item = new DataValue(new JsonValue(value, line));
            if (values.Count > 0)
            {
                if (type != item.Type)
                    throw new DataException(line);
            }
            else
            {
                type = item.Type;
            }
            values.Add(item);

        }

        public string ToString(string tabs)
        {
            StringBuilder sb = new();
            sb.Append("[\n");
            for (int i = 0; i < values.Count; i++)
            {
                DataValue item = values[i];
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

        public DataValue Query(string search)
        {
            if (type != DataType.Object && type != DataType.Array)
                return new DataValue(new JsonValue());
            foreach (DataValue item in values)
            {
                DataValue result = item.GetValue().Query(search);
                if (result.Type != DataType.Empty)
                    return result;
            }
            return new DataValue(new JsonValue());
        }
    }
}
