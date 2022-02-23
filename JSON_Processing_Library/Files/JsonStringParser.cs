using JsonProcessing.Objects;
using JsonProcessing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace JsonProcessing.Files
{
    public class JsonStringParser : IDataStringParser
    {
        private readonly DataNodeParser objectParser;
        private readonly DataNodeParser arrayParser;
        public JsonStringParser()
        {
            objectParser = new DataNodeParser(new JsonObjectParser());
            arrayParser = new DataNodeParser(new JsonArrayParser());
        }
        public DataNode ParseDataString(string dataString)
        {
            string[] jsonList = Regex.Split(dataString, @"({|}|\[|\]|,|:|""|\\|\n|null|true|false)");
            if (jsonList == null)
                throw new NullReferenceException();
            int lineCounter = 1;
            int listCounter = 0;
            try
            {
                while (listCounter < jsonList.Length)
                {
                    string target = jsonList[listCounter];
                    if (target == "\n")
                        lineCounter++;
                    else if (target == "{")
                    {
                        DataNode obj = new(new JsonObject());
                        return objectParser.ParseDataNode(obj, ref jsonList, ref lineCounter, ref listCounter);
                    }
                    else if (target == "[")
                    {
                        DataNode arr = new(new JsonArray());
                        return arrayParser.ParseDataNode(arr, ref jsonList, ref lineCounter, ref listCounter);
                    }
                    else if (!String.IsNullOrWhiteSpace(target))
                        throw new DataParserLineException(lineCounter);
                    listCounter++;
                }
                throw new DataParserLineException(lineCounter);
            }
            catch (DataParserTypeException ex)
            {
                throw new DataParserException(ex.Type.ToString(), lineCounter);
            }
        }
    }
}
