﻿using JsonProcessing.Util;
using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class JsonObject : IDataNode
    {
        private readonly Dictionary<string, DataValue> items;
        public JsonObject()
        {
            items = new Dictionary<string, DataValue>();
        }

        public void Add(string key, DataValue value)
        {
            items.Add(key, value);
        }

        public string ToString(string tabs)
        {
            StringBuilder sb = new();
            sb.Append("{\n");
            for (int i = 0; i < items.Count; i++)
            {
                KeyValuePair<string, DataValue> item = items.ElementAt(i);
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

        public DataValue Query(string search)
        {
            foreach (KeyValuePair<string, DataValue> item in items)
            {
                if (item.Key == search)
                    return item.Value;
                object value = item.Value.GetValue();
                if (value is DataNode node)
                {
                    DataValue result = node.Query(search);
                    if (result.Type != DataType.Empty)
                        return result;
                }
            }
            return new DataValue(new JsonValue());
        }

        public DataValue Get(string key)
        {
            return items[key];
        }

        public DataValue Get(int index)
        {
            return items.ElementAt(index).Value;
        }
    }
}
