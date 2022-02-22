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
        private readonly DataValueParser valueParser;

        public JsonObjectParser()
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
                else if (target == "\"")
                {
                    string key = valueParser.ParseString(ref stringList, ref lineCounter, ref listCounter);
                    DataValue value = ParseDataValue(node, ref stringList, ref lineCounter, ref listCounter);
                    listCounter++;
                    node.Add(key, value, lineCounter);
                    if (stringList[listCounter - 1] == "}")
                    {
                        return node;
                    }
                }
                else if (!String.IsNullOrWhiteSpace(target))
                {
                    throw new DataParserException(lineCounter);
                }
                listCounter++;
            }
            throw new DataParserException(lineCounter);
        }

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
                    return valueParser.ParseDataValue(node, ref stringList, ref lineCounter, ref listCounter);
                }
                else if (!String.IsNullOrWhiteSpace(subtarget))
                {
                    throw new DataParserException(lineCounter);
                }
                listCounter++;
            }
            throw new DataParserException(lineCounter);
        }
    }
}
