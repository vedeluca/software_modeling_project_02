using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public class JsonValue
    {
        private dynamic? Value { get; set; }

        public JsonValue(dynamic value, int line)
        {
            //TODO: check if it should be arrays/objects or nodes
            if (value is string ||
                value is int ||
                value is double ||
                value is bool ||
                value is JsonArray ||
                value is JsonObject ||
                value is null)
                Value = value;
            else
                throw new JsonException(line);
        }

        public dynamic? GetValue()
        {
            return Value;
        }

        new public string ToString()
        {
            return this.ToString("");
        }

        public string ToString(string tabs)
        {
            //TODO: check if it should be arrays/objects or nodes
            if (Value == null)
                return "null";
            else if (Value is string)
                return StringToJsonString((string)Value);
            else if (Value is bool)
                return Value.ToString().ToLower();
            else if (Value is JsonArray || Value is JsonObject)
                return Value.ToString(MoreTabs(tabs));
            else
                return Value.ToString();
        }

        private static string StringToJsonString(string value)
        {
            StringBuilder sb = new();
            sb.Append('"');
            sb.Append(value);
            sb.Append('"');
            return sb.ToString();
        }

        private static string MoreTabs(string tabs)
        {
            StringBuilder sb = new();
            sb.Append(tabs);
            sb.Append('\t');
            return sb.ToString();
        }
    }
}
