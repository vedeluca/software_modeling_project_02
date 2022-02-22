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
                    DataNodeParser objectParser = new(new JsonObjectParser());
                    DataNode node = new(new JsonObject(), DataType.Object, parent);
                    DataNode obj = objectParser.ParseDataNode(node, ref stringList, ref lineCounter, ref listCounter);
                    return new DataValue(new JsonValue(obj, lineCounter));
                }
                else if (target == "[")
                {
                    DataNodeParser arrayParser = new(new JsonArrayParser());
                    DataNode node = new(new JsonArray(), DataType.Array, parent);
                    DataNode arr = arrayParser.ParseDataNode(node, ref stringList, ref lineCounter, ref listCounter);
                    return new DataValue(new JsonValue(arr, lineCounter));
                }
                else if (target == "\"")
                {
                    string str = ParseString(ref stringList, ref lineCounter, ref listCounter);
                    return new DataValue(new JsonValue(str, lineCounter));
                }
                else if (int.TryParse(target, out _))
                    return new DataValue(new JsonValue(Convert.ToInt32(target), lineCounter));
                else if (double.TryParse(target, out _))
                    return new DataValue(new JsonValue(Convert.ToDouble(target), lineCounter));
                else if (target == "true")
                    return new DataValue(new JsonValue(true, lineCounter));
                else if (target == "false")
                    return new DataValue(new JsonValue(false, lineCounter));
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
