using JsonProcessing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public interface IDataValue
    {
        /// <summary>
        /// The DataType of this DataValue
        /// </summary>
        DataType Type { get; set; }

        /// <summary>
        /// The actual value of this DataValue
        /// </summary>
        /// <returns>This object should match the Type</returns>
        dynamic GetValue();

        /// <summary>
        /// Keeps track of indentation when converting nested DataValues to string
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns>The entire DataValue in string format, with proper indentation</returns>
        string ToString(string tabs);
    }
}
