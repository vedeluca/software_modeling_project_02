using JsonProcessing.Util;
using JsonProcessing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public class JsonValueParser : IDataValueParser
    {
        private readonly string[] ends = { ",", "}", "]" };
        private readonly DataNodeParser objectParser;
        private readonly DataNodeParser arrayParser;
        public JsonValueParser()
        {
            objectParser = new DataNodeParser(new JsonObjectParser());
            arrayParser = new DataNodeParser(new JsonArrayParser());
        }
        public DataValue ParseDataValue(DataNode parent, ref string[] stringList, ref int lineCounter, ref int listCounter)
        {
            DataValue value = ParseValue(parent, ref stringList, ref lineCounter, ref listCounter);
            while (listCounter < stringList.Length)
            {
                listCounter++;
                string target = stringList[listCounter];
                if (target == "\n")
                    lineCounter++;
                else if (ends.Contains(target))
                    return value;
                else if (!String.IsNullOrWhiteSpace(target))
                    throw new DataParserException(lineCounter);
            }
            throw new DataParserException(lineCounter);
        }

        private DataValue ParseValue(DataNode parent, ref string[] stringList, ref int lineCounter, ref int listCounter)
        {
            while (listCounter < stringList.Length)
            {
                string target = stringList[listCounter];
                if (target == "\n")
                    lineCounter++;
                else if (target == "{")
                {
                    DataNode obj = objectParser.ParseDataNode(parent, ref stringList, ref lineCounter, ref listCounter);
                    return new DataValue(new JsonValue(obj, DataType.Object, lineCounter));
                }
                else if (target == "[")
                {
                    DataNode arr = arrayParser.ParseDataNode(parent, ref stringList, ref lineCounter, ref listCounter);
                    return new DataValue(new JsonValue(arr, DataType.Array, lineCounter));
                }
                else if (target == "\"")
                {
                    string str = ParseString(ref stringList, ref lineCounter, ref listCounter);
                    return new DataValue(new JsonValue(str));
                }
                else if (int.TryParse(target, out _))
                    return new DataValue(new JsonValue(Convert.ToInt32(target)));
                else if (double.TryParse(target, out _))
                    return new DataValue(new JsonValue(Convert.ToDouble(target)));
                else if (target == "true")
                    return new DataValue(new JsonValue(true));
                else if (target == "false")
                    return new DataValue(new JsonValue(false));
                else if (target == "null")
                    return new DataValue(new JsonValue(DataType.Null, lineCounter));
                else if (!String.IsNullOrWhiteSpace(target))
                    throw new DataParserException(lineCounter);
                listCounter++;
            }
            throw new DataParserException(lineCounter);
        }

        public string ParseString(ref string[] stringList, ref int lineCounter, ref int listCounter)
        {
            StringBuilder sb = new StringBuilder();
            listCounter++;
            while (listCounter < stringList.Length)
            {
                string target = stringList[listCounter];
                if (target == "\"" && stringList[listCounter - 1] != "\\")
                {
                    listCounter++;
                    return sb.ToString();
                }
                if (target == "\n")
                    lineCounter++;
                sb.Append(target);
                listCounter++;
            }
            throw new DataParserException(lineCounter);
        }
    }
}
