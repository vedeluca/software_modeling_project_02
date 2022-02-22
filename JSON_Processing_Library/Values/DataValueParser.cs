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
        private IDataValueParser dataValueParser;
        public DataValueParser(IDataValueParser parser)
        {
            dataValueParser = parser;
        }
        public DataValue ParseDataValue(DataNode parent, ref string[] stringList, ref int lineCounter, ref int listCounter)
        {
            return dataValueParser.ParseDataValue(parent, ref stringList, ref lineCounter, ref listCounter);
        }

        public string ParseString(ref string[] stringList, ref int lineCounter, ref int listCounter)
        { 
            return dataValueParser.ParseString(ref stringList, ref lineCounter,ref listCounter);
        }
    }
}
