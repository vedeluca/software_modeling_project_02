using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class DataNodeParser
    {
        IDataNodeParser dataNodeParser;
        public DataNodeParser(IDataNodeParser parser)
        {
            dataNodeParser = parser;
        }
        public DataNode ParseDataNode(DataNode node, ref string[] stringList, ref int lineCounter, ref int listCounter)
        {
            return dataNodeParser.ParseDataNode(node, ref stringList, ref lineCounter, ref listCounter);
        }
    }
}
