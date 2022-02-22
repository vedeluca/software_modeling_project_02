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
        private readonly DataValueParser valueParser;

        public JsonArrayParser()
        {
            valueParser = new DataValueParser(new JsonValueParser());
        }
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
                    DataValue value = valueParser.ParseDataValue(node, ref stringList, ref lineCounter, ref listCounter);
                    node.Add("", value);
                    if (stringList[listCounter] == "]")
                    {
                        listCounter++;
                        return node;
                    }
                }
                listCounter++;
            }
            throw new DataParserException(lineCounter);
        }
    }
}
