using JsonProcessing.Util;
using JsonProcessing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public class JsonValue : IDataValue
    {
        private string stringValue;
        private int integerValue;
        private double doubleValue;
        private bool booleanValue;
        private DataNode objectValue;
        private DataNode arrayValue;
        private DataType type;
        DataType IDataValue.Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public JsonValue()
        {
            stringValue = "";
            integerValue = 0;
            doubleValue = 0;
            booleanValue = false;
            objectValue = new DataNode(new JsonObject());
            arrayValue = new DataNode(new JsonObject());
            type = DataType.Empty;
        }

        public JsonValue(string value) : this()
        {
            type = DataType.String;
            stringValue = value;
        }
        public JsonValue(int value) : this()
        {
            type = DataType.Integer;
            integerValue = value;
        }

        public JsonValue(double value) : this()
        {
            type = DataType.Double;
            doubleValue = value;
        }

        public JsonValue(bool value) : this()
        {
            type = DataType.Boolean;
            booleanValue = value;
        }
        public JsonValue(DataNode value, DataType dataType, int line) : this()
        {
            type = dataType;
            if (type == DataType.Object)
                objectValue = value;
            else if (type == DataType.Array)
                arrayValue = value;
            else
                throw new DataParserException(line);
        }

        public JsonValue(DataType dataType, int line) : this()
        {
            type = dataType;
            if (type != DataType.Null)
                throw new DataParserException(line);
        }

        public dynamic GetValue()
        {
            if (type == DataType.String)
                return stringValue;
            else if (type == DataType.Integer)
                return integerValue;
            else if (type == DataType.Double)
                return doubleValue;
            else if (type == DataType.Boolean)
                return booleanValue;
            else if (type == DataType.Object)
                return objectValue;
            else if (type == DataType.Array)
                return arrayValue;
            else
                return this;
        }

        public string ToString(string tabs)
        {
            if (type == DataType.String)
                return StringToJsonString();
            else if (type == DataType.Integer)
                return integerValue.ToString();
            else if (type == DataType.Double)
                return doubleValue.ToString();
            else if (type == DataType.Boolean)
                return booleanValue.ToString().ToLower();
            else if (type == DataType.Object)
                return objectValue.ToString(MoreTabs(tabs));
            else if (type == DataType.Array)
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

        private static string MoreTabs(string tabs)
        {
            StringBuilder sb = new();
            sb.Append(tabs);
            sb.Append('\t');
            return sb.ToString();
        }
    }
}
