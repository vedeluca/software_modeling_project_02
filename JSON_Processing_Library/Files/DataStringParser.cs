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
        IDataStringParser dataStringParser;
        public DataStringParser(IDataStringParser parser)
        {
            dataStringParser = parser;
        }
        public DataNode ParseDataString(string dataString)
        {
            return dataStringParser.ParseDataString(dataString);
        }
    }
}
