using JsonProcessing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Files
{
    public class DataFileParser
    {
        /// <summary>
        /// The parser for converting JSON files to strings
        /// </summary>
        IDataFileParser dataFileParser;

        /// <summary>
        /// Initiate the JsonFileParser
        /// </summary>
        public DataFileParser()
        {
            dataFileParser = new JsonFileParser();
        }

        /// <summary>
        /// Initiate the given parser
        /// </summary>
        /// <param name="parser"></param>
        public DataFileParser(IDataFileParser parser)
        {
            dataFileParser = parser;
        }

        /// <summary>
        /// Parse the JSON file at path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The root DataNode</returns>
        public DataNode ParseDataFile(string path)
        {
            return dataFileParser.ParseDataFile(path);
        }
    }
}
