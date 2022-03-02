using JsonProcessing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public class DataValue
    {
        /// <summary>
        /// The JsonValue injected into the constructor
        /// </summary>
        private IDataValue dataValue;

        /// <summary>
        /// The DataType of the JsonValue
        /// </summary>
        public DataType Type
        {
            get
            {
                return dataValue.Type;
            }
        }

        /// <summary>
        /// Initiate an empty JsonValue
        /// </summary>
        public DataValue()
        {
            dataValue = new JsonValue();
        }

        /// <summary>
        /// Initiate the DataValue
        /// </summary>
        /// <param name="value"></param>
        public DataValue(IDataValue value)
        {
            dataValue = value;
        }

        /// <summary>
        /// The actual value of the JsonValue
        /// </summary>
        /// <returns>This object should match this DataValue's Type</returns>
        public object GetValue()
        {
            return dataValue.GetValue();
        }

        /// <summary>
        /// Overrides the ToString method to use the following ToString method
        /// </summary>
        /// <returns>The entire DataValue in string format, with proper indentation</returns>
        public override string ToString()
        {
            return dataValue.ToString("");
        }

        /// <summary>
        /// Keeps track of indentation when converting nested DataValues to string
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns>The entire DataValue in string format, with proper indentation</returns>
        public string ToString(string tabs)
        {
            return dataValue.ToString(tabs);
        }
    }
}
