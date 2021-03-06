using JsonProcessing.Values;
using JsonProcessing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class JsonObjectParser : IDataNodeParser
    {
        /// <summary>
        /// The parser for generating DataValues from JSON strings
        /// </summary>
        private readonly DataValueParser valueParser;

        /// <summary>
        /// Initializes DataValueParser
        /// </summary>
        public JsonObjectParser()
        {
            valueParser = new DataValueParser();
        }

        /// <summary>
        /// Parse the current DataNode while looking for the end of the object
        /// </summary>
        /// <param name="node"></param>
        /// <param name="stringList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <returns>A new DataNode</returns>
        /// <exception cref="DataParserLineException"></exception>
        public DataNode ParseDataNode(DataNode node, ref string[] stringList, ref int lineCounter, ref int listCounter)
        {
            listCounter++;
            while (listCounter < stringList.Length)
            {
                string target = stringList[listCounter];
                if (target == "\n")
                {
                    lineCounter++;
                }
                else if (target == "\"")
                {
                    string key = valueParser.ParseString(ref stringList, ref lineCounter, ref listCounter);
                    DataValue value = ParseDataValue(node, ref stringList, ref lineCounter, ref listCounter);
                    listCounter++;
                    node.Add(key, value);
                    if (stringList[listCounter - 1] == "}")
                    {
                        return node;
                    }
                }
                else if (!String.IsNullOrWhiteSpace(target))
                {
                    throw new DataParserLineException(lineCounter);
                }
                listCounter++;
            }
            throw new DataParserLineException(lineCounter);
        }

        /// <summary>
        /// Check for colon before parsing the DataValue
        /// </summary>
        /// <param name="node"></param>
        /// <param name="stringList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <returns>A new DataValue</returns>
        /// <exception cref="DataParserLineException"></exception>
        private DataValue ParseDataValue(DataNode node, ref string[] stringList, ref int lineCounter, ref int listCounter)
        {
            listCounter++;
            while (listCounter < stringList.Length)
            {
                string subtarget = stringList[listCounter];
                if (subtarget == "\n")
                {
                    lineCounter++;
                }
                else if (subtarget == ":")
                {
                    listCounter++;
                    return valueParser.ParseDataValue(node, ref stringList, ref lineCounter, ref listCounter, "}");
                }
                else if (!String.IsNullOrWhiteSpace(subtarget))
                {
                    throw new DataParserLineException(lineCounter);
                }
                listCounter++;
            }
            throw new DataParserLineException(lineCounter);
        }
    }
}
