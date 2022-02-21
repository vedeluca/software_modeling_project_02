using JsonProcessing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public class JsonValue : DataValue
    {
        private string stringValue;
        private int integerValue;
        private double doubleValue;
        private bool booleanValue;
        private JsonObject objectValue;
        private JsonArray arrayValue;
        private new JsonType type;

        public JsonValue() : base()
        {
            stringValue = "";
            integerValue = 0;
            doubleValue = 0;
            booleanValue = false;
            objectValue = new JsonObject();
            arrayValue = new JsonArray();
        }

        public JsonValue(dynamic? value, int line)
            : this()
        {
            if (value is string)
            {
                type = JsonType.String;
                stringValue = value;
            }
            else if (value is int)
            {
                type = JsonType.Integer;
                integerValue = value;
            }
            else if (value is double)
            {
                type = JsonType.Double;
                doubleValue = value;
            }
            else if (value is bool)
            {
                type = JsonType.Boolean;
                booleanValue = value;
            }
            else if (value is JsonArray)
            {
                type = JsonType.Array;
                arrayValue = value;
            }
            else if (value is JsonObject)
            {
                type = JsonType.Object;
                objectValue = value;
            }
            else if (value is null)
            {
                type = JsonType.Null;
            }
            else
            {
                throw new DataException(line);
            }
        }

        public override dynamic GetValue()
        {
            if (type == JsonType.String)
                return stringValue;
            else if (type == JsonType.Integer)
                return integerValue;
            else if (type == JsonType.Double)
                return doubleValue;
            else if (type == JsonType.Boolean)
                return booleanValue;
            else if (type == JsonType.Object)
                return objectValue;
            else if (type == JsonType.Array)
                return arrayValue;
            else
                return this;
        }

        public override Enum GetType()
        {
            return type;
        }

        public override string ToString()
        {
            return this.ToString("");
        }

        public override string ToString(string tabs)
        {
            if (type == JsonType.String)
                return StringToJsonString();
            else if (type == JsonType.Integer)
                return integerValue.ToString();
            else if (type == JsonType.Double)
                return doubleValue.ToString();
            else if (type == JsonType.Boolean)
                return booleanValue.ToString().ToLower();
            else if (type == JsonType.Object)
                return objectValue.ToString(MoreTabs(tabs));
            else if (type == JsonType.Array)
                return arrayValue.ToString(MoreTabs(tabs));
            else
                return "null";
        }

        private string StringToJsonString()
        {
            StringBuilder sb = new();
            sb.Append('"');
            sb.Append(stringValue);
            sb.Append('"');
            return sb.ToString();
        }

        protected override string MoreTabs(string tabs)
        {
            StringBuilder sb = new();
            sb.Append(tabs);
            sb.Append('\t');
            return sb.ToString();
        }
    }
}
