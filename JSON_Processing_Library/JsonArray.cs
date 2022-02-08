using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing
{
    internal class JsonArray<T> : List<T>, JsonNode
    {
        public JsonNode? Parent { get; set; }
        public JsonNode? Root { get; set; }

        public JsonArray() { }

        public JsonArray(JsonNode parent)
        {
            Parent = parent;
            Root = (parent.Root != null) ? parent.Root : parent;
        }

        new public string ToString()
        {
            return this.ToString("");
        }

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
                else if (item is JsonNode)
                    sb.Append(JsonUtility.NodeToString((JsonNode)item, tabs));
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

        public object Query(string search)
        {
            Type genericType = typeof(T);
            Type interfaceType = typeof(JsonNode);
            if (interfaceType.IsAssignableFrom(genericType))
            {
                foreach (JsonNode? node in this)
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
