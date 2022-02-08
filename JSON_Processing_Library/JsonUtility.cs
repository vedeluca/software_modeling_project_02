using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing
{
    internal static class JsonUtility
    {
        public static string StringToString(string value)
        {
            StringBuilder sb = new();
            sb.Append('"');
            sb.Append(value);
            sb.Append('"');
            return sb.ToString();
        }

        public static string NodeToString(JsonNode node, string tabs)
        {
            StringBuilder tabsBuilder = new();
            tabsBuilder.Append(tabs);
            tabsBuilder.Append('\t');
            return node.ToString(tabsBuilder.ToString());
        }

        public static string QueryToString(this JsonNode node, string search)
        {
            object? query = node.Query(search);
            if (query == null)
                return "null";
            else if (query is JsonNode)
                return ((JsonNode)query).ToString();
            else
                return query.ToString();
        }
    }
}
