using JsonProcessing.Util;
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
        /// <summary>
        /// A dictionary of DataValues used by the JsonObject
        /// </summary>
        private readonly Dictionary<string, DataValue> items;

        /// <summary>
        /// The number of items in the dictionary
        /// </summary>
        public int Count { get { return items.Count; } }

        /// <summary>
        /// Initialize the dictionary of DataValues
        /// </summary>
        public JsonObject()
        {
            items = new Dictionary<string, DataValue>();
        }

        /// <summary>
        /// Adds to dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, DataValue value)
        {
            items.Add(key, value);
        }

        /// <summary>
        /// Keeps track of indentation when converting nested DataNodes to string
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns>The entire node in string format, with proper indentation</returns>
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

        /// <summary>
        /// Searches the node and all of its children for a value with the key of "search"
        /// </summary>
        /// <param name="search"></param>
        /// <returns>The DataValue being searched or an empty DataValue</returns>
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
            return new DataValue();
        }

        /// <summary>
        /// Get the DataValue with matching key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>DataValue</returns>
        public DataValue GetValueAt(string key)
        {
            return items[key];
        }

        /// <summary>
        /// Get the DataValue at position
        /// </summary>
        /// <param name="index"></param>
        /// <returns>DataValue</returns>
        public DataValue GetValueAt(int index)
        {
            return items.ElementAt(index).Value;
        }

        /// <summary>
        /// Get the key at position
        /// </summary>
        /// <param name="index"></param>
        /// <returns>key</returns>
        public string GetKeyAt(int index)
        {
            return items.ElementAt(index).Key;
        }
    }
}
