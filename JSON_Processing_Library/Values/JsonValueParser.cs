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
        public DataValue ParseDataValue(DataNode parent, ref string[] stringList, ref int lineCounter, ref int listCounter, string end)
        {
            List<string> ends = new();
            ends.Add(",");
            ends.Add(end);
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
                    throw new DataParserLineException(lineCounter);
            }
            throw new DataParserLineException(lineCounter);
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
                    DataNode node = new(new JsonObject(), parent);
                    DataNode obj = objectParser.ParseDataNode(node, ref stringList, ref lineCounter, ref listCounter);
                    return new DataValue(new JsonValue(obj));
                }
                else if (target == "[")
                {
                    DataNodeParser arrayParser = new(new JsonArrayParser());
                    DataNode node = new(new JsonArray(), parent);
                    DataNode arr = arrayParser.ParseDataNode(node, ref stringList, ref lineCounter, ref listCounter);
                    return new DataValue(new JsonValue(arr));
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
                    return new DataValue(new JsonValue(DataType.Null));
                else if (!String.IsNullOrWhiteSpace(target))
                    throw new DataParserLineException(lineCounter);
                listCounter++;
            }
            throw new DataParserLineException(lineCounter);
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
            throw new DataParserLineException(lineCounter);
        }
    }
}
