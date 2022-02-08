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
            Root = (parent.Root != null) ? parent.Root : parent;
        }

        new public string ToString()
        {
            return this.ToString("");
        }

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
                else if (item.Value is JsonNode)
                    sb.Append(JsonUtility.NodeToString((JsonNode)item.Value, tabs));
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
            //TODO: string handler with exception? Don't do if not enough time (talking about the escaped characters)
            if (value is string ||
                value is int ||
                value is double ||
                value is bool ||
                value is JsonNode ||
                value is null)
                base.Add(key, value);
            else
                throw new NotImplementedException();
        }
    }
}
