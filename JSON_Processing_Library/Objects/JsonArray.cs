using JsonProcessing.Util;
using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class JsonArray : IDataNode
    {
        /// <summary>
        /// A list of DataValues used by the JsonArray
        /// </summary>
        private readonly List<DataValue> values;

        /// <summary>
        /// The type of DataValues stored in the JsonArray
        /// </summary>
        private DataType type;

        /// <summary>
        /// The number of items in the list
        /// </summary>
        public int Count { get { return values.Count; } }

        /// <summary>
        /// Initialize the list of DataValues and set the initial type to null
        /// </summary>
        public JsonArray()
        {
            values = new List<DataValue>();
            type = DataType.Null;
        }

        /// <summary>
        /// Key is ignored when adding a DataValue to the JsonArray
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, DataValue value)
        {
            Add(value);
        }

        /// <summary>
        /// Check the DataType then add the DataValue to the list
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="DataParserTypeException"></exception>
        public void Add(DataValue value)
        {
            if (values.Count > 0)
            {
                if (type != value.Type)
                    throw new DataParserTypeException(DataType.Array);
            }
            else
            {
                type = value.Type;
            }
            values.Add(value);

        }

        /// <summary>
        ///  Keeps track of indentation when converting nested DataNodes to string
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns>The entire node in string format, with proper indentation</returns>
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

        /// <summary>
        /// Searches the node and all of its children for a value with the key of "search"
        /// </summary>
        /// <param name="search"></param>
        /// <returns>The DataValue being searched or an empty DataValue</returns>
        public DataValue Query(string search)
        {
            if (type != DataType.Object && type != DataType.Array)
                return new DataValue();
            foreach (DataValue item in values)
            {
                DataNode node = (DataNode)item.GetValue();
                DataValue result = node.Query(search);
                if (result.Type != DataType.Empty)
                    return result;
            }
            return new DataValue();
        }

        /// <summary>
        /// If the key can convert to an int, returns the DataValue at that position
        /// </summary>
        /// <param name="key"></param>
        /// <returns>DataValue</returns>
        /// <exception cref="ArgumentException"></exception>
        public DataValue GetValueAt(string key)
        {
            if (int.TryParse(key, out _))
                return values[Convert.ToInt32(key)];
            else
                throw new ArgumentException(String.Format("Parameter {0} needs to be an integer for JsonArray.Get()", key));
        }

        /// <summary>
        /// Get the DataValue at position
        /// </summary>
        /// <param name="index"></param>
        /// <returns>DataValue</returns>
        public DataValue GetValueAt(int index)
        {
            return values[index];
        }

        /// <summary>
        /// Just returns index as a string.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetKeyAt(int index)
        {
            return index.ToString();
        }
    }
}
