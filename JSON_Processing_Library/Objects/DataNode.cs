using JsonProcessing.Values;
using JsonProcessing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class DataNode
    {
        /// <summary>
        /// The JsonObject or JsonArray injected into the constructor
        /// </summary>
        public IDataNode Node { get; set; }

        /// <summary>
        /// The direct parent of the current node
        /// </summary>
        public DataNode? Parent { get; set; }

        /// <summary>
        /// The root of the entire JSON tree
        /// </summary>
        public DataNode? Root { get; set; }
        
        /// <summary>
        /// The number of items in the node
        /// </summary>
        public int Count
        {
            get
            {
                return Node.Count;
            }
        }

        /// <summary>
        /// Initiate data node if there is no parent. This is used for the root node.
        /// </summary>
        /// <param name="node"></param>
        public DataNode(IDataNode node) : base()
        {
            Node = node;
        }

        /// <summary>
        /// Initiate data node with parent. If the parent has no root, the root is the parent.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parent"></param>
        public DataNode(IDataNode node, DataNode parent) : this(node)
        {
            Parent = parent;
            Root = parent.Root ?? parent;
        }

        /// <summary>
        /// Add key and value to the node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, DataValue value)
        {
            Node.Add(key, value);
        }

        /// <summary>
        /// Add just the value to the node
        /// </summary>
        /// <param name="value"></param>
        public void Add(DataValue value)
        {
            Node.Add("", value);
        }

        /// <summary>
        /// Overrides the ToString method to use the following ToString method
        /// </summary>
        /// <returns>The entire node in string format, with proper indentation</returns>
        public override string ToString()
        {
            return Node.ToString("");
        }

        /// <summary>
        /// Keeps track of indentation when converting nested DataNodes to string
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns>The entire node in string format, with proper indentation</returns>
        public string ToString(string tabs)
        {
            return Node.ToString(tabs);
        }
        /// <summary>
        /// Searches the node and all of its children for a value with the key of "search"
        /// </summary>
        /// <param name="search"></param>
        /// <returns>The DataValue being searched</returns>
        public DataValue Query(string search)
        {
            return Node.Query(search);
        }

        /// <summary>
        /// Get the DataValue with the given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>DataValue</returns>
        public DataValue GetValueAt(string key)
        {
            return Node.GetValueAt(key);
        }

        /// <summary>
        /// Get the DataValue at position
        /// </summary>
        /// <param name="index"></param>
        /// <returns>DataValue</returns>
        public DataValue GetValueAt(int index)
        {
            return Node.GetValueAt(index);
        }

        /// <summary>
        /// Get the key at position
        /// </summary>
        /// <param name="index"></param>
        /// <returns>key</returns>
        public string GetKeyAt(int index)
        {
            return Node.GetKeyAt(index);
        }
    }
}
