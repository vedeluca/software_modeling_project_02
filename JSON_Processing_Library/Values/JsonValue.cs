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
        /// <summary>
        /// These values are all possible values for an instance of this class
        /// </summary>
        private string stringValue;
        private int integerValue;
        private double doubleValue;
        private bool booleanValue;
        private DataNode objectValue;
        private DataNode arrayValue;

        /// <summary>
        /// This DataType determines while value is being used
        /// </summary>
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

        /// <summary>
        /// All values initiated as empty and the type is set to Empty
        /// </summary>
        public JsonValue()
        {
            stringValue = "";
            integerValue = 0;
            doubleValue = 0;
            booleanValue = false;
            objectValue = new DataNode(new JsonObject());
            arrayValue = new DataNode(new JsonArray());
            type = DataType.Empty;
        }

        /// <summary>
        /// Determines which value is used and what the type is
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="DataParserTypeException"></exception>
        public JsonValue(object value)
             : this()
        {
            if (value is string @string)
            {
                type = DataType.String;
                stringValue = @string;
            }
            else if (value is int @int)
            {
                type = DataType.Integer;
                integerValue = @int;
            }
            else if (value is double @double)
            {
                type = DataType.Double;
                doubleValue = @double;
            }
            else if (value is bool boolean)
            {
                type = DataType.Boolean;
                booleanValue = boolean;
            }
            else if (value is DataNode data)
            {
                if (data.Node is JsonObject)
                {
                    type = DataType.Object;
                    objectValue = data;
                }
                else if (data.Node is JsonArray)
                {
                    type = DataType.Array;
                    arrayValue = data;
                }
                else
                    throw new DataParserTypeException(DataType.Value);
            }
            else if (value is DataType dataType && dataType == DataType.Null)
                type = DataType.Null;
            else
            {
                throw new DataParserTypeException(DataType.Value);
            }
        }

        /// <summary>
        /// Get the actual value
        /// </summary>
        /// <returns>If the value is null or empty, the type is checked</returns>
        public object GetValue()
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

        /// <summary>
        /// Keeps track of indentation when converting nested JsonValues to string
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns>The entire JsonValue in string format, with proper indentation</returns>
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

        /// <summary>
        /// Puts quotation marks back onto the string
        /// </summary>
        /// <returns>The stringValue with quotation marks</returns>
        private string StringToJsonString()
        {
            StringBuilder sb = new();
            sb.Append('"');
            sb.Append(stringValue);
            sb.Append('"');
            return sb.ToString();
        }

        /// <summary>
        /// Adds tabs when further indenting
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns>Several \t characters</returns>
        private static string MoreTabs(string tabs)
        {
            StringBuilder sb = new();
            sb.Append(tabs);
            sb.Append('\t');
            return sb.ToString();
        }
    }
}
