using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing
{
    internal class JsonObject<TKey, TValue> : Dictionary<string, object?>, JsonNode
    {
        public JsonNode? Parent { get; set; }
        public JsonNode? Root { get; set; }

        public JsonObject() { }

        public JsonObject(JsonNode parent)
        {
            Parent = parent;
            if (parent.Root != null)
                Root = parent.Root;
            else
                Root = parent;
        }

        //TODO: bring back and finish the ToString()

        public object? Query(string search)
        {
            foreach (KeyValuePair<string, object?> pair in this)
            {
                if (pair.Key == search)
                    return pair.Value;
                if (pair.Value is JsonNode jsonNode)
                {
                    object? queryResult = jsonNode.Query(search);
                    if (queryResult != null)
                        return queryResult;
                }
            }
            return null;
        }
        new public void Add(string key, object? value)
        {
            if (value is string ||
                value is int ||
                value is float ||
                value is bool ||
                value is JsonNode ||
                value is null)
                base.Add(key, value);
            else
                throw new NotImplementedException();
        }
    }
}
