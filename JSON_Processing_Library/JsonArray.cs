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
            if (parent.Root != null)
                Root = parent.Root;
            else
                Root = parent;
        }

        public string ToString()
        {
            return "";
        }

        public string QueryToString(string search)
        {
            object? query = Query(search);
            if (query == null)
                return "null";
            else
                return query.ToString();
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
