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

        public string QueryToString(string search)
        {
            object? query = Query(search);
            if (query == null)
                return "null";
            else if (query is JsonNode)
                return ((JsonNode)query).ToString();
            else
                return query.ToString();
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
                {
                    sb.Append("null");
                }
                else if (item.Value is string)
                {
                    sb.Append('"');
                    sb.Append(item.Value);
                    sb.Append('"');
                }
                else if (item.Value is JsonNode)
                {
                    StringBuilder tabsBuilder = new();
                    tabsBuilder.Append(tabs);
                    tabsBuilder.Append('\t');
                    JsonNode node = (JsonNode)item.Value;
                    sb.Append(node.ToString(tabsBuilder.ToString()));
                }
                else
                {
                    sb.Append(item.Value.ToString());
                }
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
