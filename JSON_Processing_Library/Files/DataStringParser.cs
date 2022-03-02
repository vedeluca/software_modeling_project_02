using JsonProcessing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Files
{
    public class DataStringParser
    {
        /// <summary>
        /// The parser for converting JSON strings to DataNodes
        /// </summary>
        IDataStringParser dataStringParser;

        /// <summary>
        /// Initiate the JsoneStringParser
        /// </summary>
        public DataStringParser()
        {
            dataStringParser = new JsonStringParser();
        }

        /// <summary>
        /// Initiate the given parser
        /// </summary>
        /// <param name="parser"></param>
        public DataStringParser(IDataStringParser parser)
        {
            dataStringParser = parser;
        }

        /// <summary>
        /// Convert JSON string to DataNode
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns>The root DataNode</returns>
        public DataNode ParseDataString(string dataString)
        {
            return dataStringParser.ParseDataString(dataString);
        }
    }
}
