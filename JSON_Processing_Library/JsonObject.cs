using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing
{
    internal class JsonObject<TKey, TValue> : Dictionary<string, object?>, IJsonNode
    {
        /// <summary>
        /// The direct parent of the current node
        /// </summary>
        public IJsonNode? Parent { get; set; }

        /// <summary>
        /// The root of the entire JSON tree
        /// </summary>
        public IJsonNode? Root { get; set; }

        public JsonObject() { }

        /// <summary>
        /// If this JSON object's parent has no root, then the parent is the root
        /// </summary>
        /// <param name="parent"></param>
        public JsonObject(IJsonNode parent)
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
            sb.Append("{\n");
            for (int i = 0; i < this.Count; i++)
            {
                KeyValuePair<string, object?> item = this.ElementAt(i);
                sb.Append(tabs);
                sb.Append("\t\"");
                sb.Append(item.Key);
                sb.Append("\": ");
                if (item.Value == null)
                    sb.Append("null");
                else if (item.Value is string)
                    sb.Append(JsonUtility.StringToString(item.Value.ToString()));
                else if (item.Value is bool)
                    sb.Append(item.Value.ToString().ToLower());
                else if (item.Value is IJsonNode)
                    sb.Append(JsonUtility.NodeToString((IJsonNode)item.Value, tabs));
                else
                    sb.Append(item.Value.ToString());
                if (i < this.Count - 1)
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
        /// <returns>The object being searched, or null</returns>
        public object? Query(string search)
        {
            foreach (KeyValuePair<string, object?> pair in this)
            {
                if (pair.Key == search)
                    return pair.Value;
                if (pair.Value is IJsonNode jsonNode)
                {
                    object? queryResult = jsonNode.Query(search);
                    if (queryResult != null)
                        return queryResult;
                }
            }
            return null;
        }

        /// <summary>
        /// Replaces the Add method so that the value type is checked
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">Must be a string, int, double, bool, JsonNode, or null</param>
        /// <exception cref="NotImplementedException"></exception>
        new public void Add(string key, object? value)
        {
            if (value is string ||
                value is int ||
                value is double ||
                value is bool ||
                value is IJsonNode ||
                value is null)
                base.Add(key, value);
            else
                throw new NotImplementedException();
        }
    }
}
