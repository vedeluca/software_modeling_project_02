using JsonProcessing.Values;
using JsonProcessing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class JsonArrayParser : IDataNodeParser
    {
        /// <summary>
        /// The parser for generating DataValues from JSON strings
        /// </summary>
        private readonly DataValueParser valueParser;

        /// <summary>
        /// Initializes DataValueParser
        /// </summary>
        public JsonArrayParser()
        {
            valueParser = new DataValueParser();
        }

        /// <summary>
        /// Parse the current DataNode while looking for the end of the array
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
                else if (!String.IsNullOrWhiteSpace(target))
                {
                    DataValue value = valueParser.ParseDataValue(node, ref stringList, ref lineCounter, ref listCounter, "]");
                    node.Add(value);
                    if (stringList[listCounter] == "]")
                    {
                        listCounter++;
                        return node;
                    }
                }
                listCounter++;
            }
            throw new DataParserLineException(lineCounter);
        }
    }
}
