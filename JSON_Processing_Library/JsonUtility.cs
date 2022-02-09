using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing
{
    internal static class JsonUtility
    {
        /// <summary>
        /// Adds quatation marks back onto the ends of the string
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Original value with quotation marks</returns>
        public static string StringToString(string value)
        {
            StringBuilder sb = new();
            sb.Append('"');
            sb.Append(value);
            sb.Append('"');
            return sb.ToString();
        }

        /// <summary>
        /// Adds the appropriate number of tabs to each line of the 
        /// string output of a JsonNode
        /// </summary>
        /// <param name="node" cref="IJsonNode"></param>
        /// <param name="tabs"></param>
        /// <returns>The JsonNode, converted to string, with each line
        /// containing the correct amount of indentation depending on
        /// how deep the node is nested</returns>
        public static string NodeToString(IJsonNode node, string tabs)
        {
            StringBuilder tabsBuilder = new();
            tabsBuilder.Append(tabs);
            tabsBuilder.Append('\t');
            return node.ToString(tabsBuilder.ToString());
        }

        /// <summary>
        /// Extends JsonNodes with a method that converts the query result to a string
        /// </summary>
        /// <param name="node" cref="IJsonNode"></param>
        /// <param name="search"></param>
        /// <returns>The query result converted to a string with the ToString method,
        /// or the string "null" for a null result</returns>
        public static string QueryToString(this IJsonNode node, string search)
        {
            object? query = node.Query(search);
            if (query == null)
                return "null";
            else if (query is IJsonNode)
                return ((IJsonNode)query).ToString();
            else
                return query.ToString();
        }
    }
}
