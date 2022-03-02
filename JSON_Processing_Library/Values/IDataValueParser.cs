using JsonProcessing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public interface IDataValueParser
    {
        /// <summary>
        /// Parse the value in the current DataNode
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="stringList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <param name="end"></param>
        /// <returns>The parsed DataValue</returns>
        DataValue ParseDataValue(DataNode parent, ref string[] stringList, ref int lineCounter, ref int listCounter, string end);

        /// <summary>
        /// Parses a string withing the data string
        /// </summary>
        /// <param name="stringList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <returns>A properly formatted string</returns>
        string ParseString(ref string[] stringList, ref int lineCounter, ref int listCounter);
    }
}
