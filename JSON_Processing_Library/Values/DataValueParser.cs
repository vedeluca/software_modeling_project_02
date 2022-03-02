using JsonProcessing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public class DataValueParser
    {
        /// <summary>
        /// The parser for generating DataValues from JSON strings
        /// </summary>
        private IDataValueParser dataValueParser;

        /// <summary>
        /// Initiate the JsonValueParser
        /// </summary>
        public DataValueParser()
        {
            dataValueParser = new JsonValueParser();
        }

        /// <summary>
        /// Initiate the parser
        /// </summary>
        /// <param name="parser"></param>
        public DataValueParser(IDataValueParser parser)
        {
            dataValueParser = parser;
        }

        /// <summary>
        /// Parse the value in the current DataNode
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="stringList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <param name="end"></param>
        /// <returns>The parsed DataValue</returns>
        public DataValue ParseDataValue(DataNode parent, ref string[] stringList, ref int lineCounter, ref int listCounter, string end)
        {
            return dataValueParser.ParseDataValue(parent, ref stringList, ref lineCounter, ref listCounter, end);
        }

        /// <summary>
        /// Parses a string withing the data string
        /// </summary>
        /// <param name="stringList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <returns>A properly formatted string</returns>
        public string ParseString(ref string[] stringList, ref int lineCounter, ref int listCounter)
        {
            return dataValueParser.ParseString(ref stringList, ref lineCounter, ref listCounter);
        }
    }
}
