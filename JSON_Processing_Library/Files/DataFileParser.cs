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
        IDataFileParser dataFileParser;

        public DataFileParser()
        {
            dataFileParser = new JsonFileParser();
        }
        public DataFileParser(IDataFileParser parser)
        {
            dataFileParser = parser;
        }
        public DataNode ParseDataFile(string path)
        {
            return dataFileParser.ParseDataFile(path);
        }
    }
}
