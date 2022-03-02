using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class DataNodeParser
    {
        /// <summary>
        /// The parser for generating DataNodes from JSON strings
        /// </summary>
        IDataNodeParser dataNodeParser;

        /// <summary>
        /// Initiate the JsonArrayParser or JsonObjectParser
        /// </summary>
        /// <param name="parser"></param>
        public DataNodeParser(IDataNodeParser parser)
        {
            dataNodeParser = parser;
        }

        /// <summary>
        /// Parse the current DataNode
        /// </summary>
        /// <param name="node"></param>
        /// <param name="stringList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <returns>A new DataNode</returns>
        public DataNode ParseDataNode(DataNode node, ref string[] stringList, ref int lineCounter, ref int listCounter)
        {
            return dataNodeParser.ParseDataNode(node, ref stringList, ref lineCounter, ref listCounter);
        }
    }
}
