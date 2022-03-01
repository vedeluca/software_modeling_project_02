using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public interface IDataNode
    {
        /// <summary>
        /// The number of items in the node
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Add key and value to the node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(string key, DataValue value);

        /// <summary>
        /// Keeps track of indentation when converting nested DataNodes to string
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns>The entire node in string format, with proper indentation</returns>
        string ToString(string tabs);

        /// <summary>
        /// Searches the node and all of its children for a value with the key of "search"
        /// </summary>
        /// <param name="search"></param>
        /// <returns>The DataValue being searched</returns>
        DataValue Query(string search);

        /// <summary>
        /// Get the DataValue with the given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>DataValue</returns>
        DataValue GetValueAt(string key);

        /// <summary>
        /// Get the DataValue at position
        /// </summary>
        /// <param name="index"></param>
        /// <returns>DataValue</returns>
        DataValue GetValueAt(int index);

        /// <summary>
        /// Get the key at position
        /// </summary>
        /// <param name="index"></param>
        /// <returns>key</returns>
        string GetKeyAt(int index);
    }
}
