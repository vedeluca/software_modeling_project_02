using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing
{
    internal class JsonArray<T> : List<T>, IJsonNode
    {
        /// <summary>
        /// The direct parent of the current node
        /// </summary>
        public IJsonNode? Parent { get; set; }

        /// <summary>
        /// The root of the entire JSON tree
        /// </summary>
        public IJsonNode? Root { get; set; }

        public JsonArray() { }

        /// <summary>
        /// If this JSON array's parent has no root, then the parent is the root
        /// </summary>
        /// <param name="parent"></param>
        public JsonArray(IJsonNode parent)
        {
            Parent = parent;
            Root = (parent.Root != null) ? parent.Root : parent;
        }

        /// <summary>
        /// Replaces the ToString method
        /// </summary>
        /// <returns>The entire node in string format, with proper indentation</returns>
        new public string ToString()
        {
            return this.ToString("");
        }

        /// <summary>
        /// Keeps track of indentation when converting nested JsonNodes to string
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns>The entire node in string format, with proper indentation</returns>
        public string ToString(string tabs)
        {
            StringBuilder sb = new();
            sb.Append("[\n");
            for (int i = 0; i < this.Count; i++)
            {
                sb.Append(tabs);
                sb.Append('\t');
                T item = this[i];
                if (item == null)
                    sb.Append("null");
                else if (item is string)
                    sb.Append(JsonUtility.StringToString(item.ToString()));
                else if (item is bool)
                    sb.Append(item.ToString().ToLower());
                else if (item is IJsonNode)
                    sb.Append(JsonUtility.NodeToString((IJsonNode)item, tabs));
                else
                    sb.Append(item.ToString());
                if (i < this.Count - 1)
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
        /// <returns>The object being searched, or null</returns>
        public object? Query(string search)
        {
            Type genericType = typeof(T);
            Type interfaceType = typeof(IJsonNode);
            if (interfaceType.IsAssignableFrom(genericType))
            {
                foreach (IJsonNode? node in this)
                {
                    if (null != node)
                    {
                        object queryResult = node.Query(search);
                        if (queryResult != null)
                            return queryResult;
                    }
                }
            }
            return null;
        }
    }
}
