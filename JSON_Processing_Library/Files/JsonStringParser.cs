using JsonProcessing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace JsonProcessing.Files
{
    public static class JsonStringParser
    {
        /*public static DataNode StringToNode(string dataString)
        {
            string[] jsonList = Regex.Split(dataString, @"({|}|\[|\]|,|:|""|\\|\n|null|true|false)");
            if (jsonList == null)
                throw new NullReferenceException();
            int lineCounter = 1;
            int listCounter = 0;
            while (listCounter < jsonList.Length)
            {
                string target = jsonList[listCounter];
                if (target == "\n")
                    lineCounter++;
                else if (target == "{")

            }
        }
        public static IJsonNode? StringToJsonNode(string jsonString)
        {
            string[] jsonList = Regex.Split(jsonString, @"({|}|\[|\]|,|:|""|\\|\n|null|true|false)");
            if (jsonList == null)
                throw new NullReferenceException();
            int lineCounter = 1;
            int listCounter = 0;
            while (listCounter < jsonList.Length)
            {
                string target = jsonList[listCounter];
                if (target == "\n")
                    lineCounter++;
                else if (target == "{")
                    return new JsonObject<string, object?>().ParseJsonObject(ref jsonList, ref lineCounter, ref listCounter);
                else if (target == "[")
                    return new JsonArray<object?>().ParseJsonArray(ref jsonList, ref lineCounter, ref listCounter);
                else if (!String.IsNullOrWhiteSpace(target))
                    throw new JsonParserException(lineCounter);
                listCounter++;
            }
            throw new JsonParserException(lineCounter);
        }*/
    }
}
