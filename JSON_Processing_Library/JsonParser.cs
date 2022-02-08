using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace JsonProcessing
{
    internal static class JsonParser
    {
        public static JsonNode? StringToJsonNode(string jsonString)
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
        }

        private static JsonObject<string, object?> ParseJsonObject(this JsonObject<string, object?> jsonObject, ref string[] jsonList, ref int lineCounter, ref int listCounter)
        {
            listCounter++;
            while (listCounter < jsonList.Length)
            {
                string target = jsonList[listCounter];
                if (target == "\n")
                {
                    lineCounter++;
                }
                else if (target == "\"")
                {
                    string key = ParseString(ref jsonList, ref lineCounter, ref listCounter);
                    object? value = ParseObjVal(jsonObject, ref jsonList, ref lineCounter, ref listCounter);
                    listCounter++;
                    jsonObject.Add(key, value);
                    if (jsonList[listCounter - 1] == "}")
                    {
                        return jsonObject;
                    }
                }
                else if (!String.IsNullOrWhiteSpace(target))
                {
                    throw new JsonParserException(lineCounter);
                }
                listCounter++;
            }
            throw new JsonParserException(lineCounter);
        }

        private static JsonArray<object?> ParseJsonArray(this JsonArray<object?> jsonArray, ref string[] jsonList, ref int lineCounter, ref int listCounter)
        {
            Type type = null;
            listCounter++;
            while (listCounter < jsonList.Length)
            {
                string target = jsonList[listCounter];
                if (target == "\n")
                {
                    lineCounter++;
                }
                else if (!String.IsNullOrWhiteSpace(target))
                {
                    object? value = ParseValueAndEnd(jsonArray, "]", ref jsonList, ref lineCounter, ref listCounter);
                    if (value == null && type != null)
                        throw new JsonParserException(lineCounter);
                    else if (value != null)
                    {
                        if (type == null)
                            type = value.GetType();
                        else if (!type.Equals(value.GetType()))
                            throw new JsonParserException(lineCounter);
                    }
                    jsonArray.Add(value);
                    if (jsonList[listCounter] == "]")
                    {
                        listCounter++;
                        return jsonArray;
                    }
                }
                listCounter++;

            }
            throw new JsonParserException(lineCounter);
        }

        private static object? ParseObjVal(JsonObject<string, object?> jsonObject, ref string[] jsonList, ref int lineCounter, ref int listCounter)
        {
            listCounter++;
            while (listCounter < jsonList.Length)
            {
                string subtarget = jsonList[listCounter];
                if (subtarget == "\n")
                {
                    lineCounter++;
                }
                else if (subtarget == ":")
                {
                    listCounter++;
                    return ParseValueAndEnd(jsonObject, "}", ref jsonList, ref lineCounter, ref listCounter);
                }
                else if (!String.IsNullOrWhiteSpace(subtarget))
                {
                    throw new JsonParserException(lineCounter);
                }
                listCounter++;
            }
            throw new JsonParserException(lineCounter);
        }

        private static object? ParseValueAndEnd(JsonNode parent, string end, ref string[] jsonList, ref int lineCounter, ref int listCounter)
        {
            object? value = ParseValue(parent, ref jsonList, ref lineCounter, ref listCounter);
            while (listCounter < jsonList.Length)
            {
                listCounter++;
                string target = jsonList[listCounter];
                if (target == "\n")
                {
                    lineCounter++;
                }
                else if (target == "," || target == end)
                {
                    return value;
                }
                else if (!String.IsNullOrWhiteSpace(target))
                {
                    throw new JsonParserException(lineCounter);
                }
            }
            throw new JsonParserException(lineCounter);
        }

        private static object? ParseValue(JsonNode parent, ref string[] jsonList, ref int lineCounter, ref int listCounter)
        {
            while (listCounter < jsonList.Length)
            {
                string target = jsonList[listCounter];
                if (target == "\n")
                    lineCounter++;
                else if (target == "{")
                    return new JsonObject<string, object?>(parent).ParseJsonObject(ref jsonList, ref lineCounter, ref listCounter);
                else if (target == "[")
                    return new JsonArray<object?>(parent).ParseJsonArray(ref jsonList, ref lineCounter, ref listCounter);
                else if (target == "\"")
                    return ParseString(ref jsonList, ref lineCounter, ref listCounter);
                else if (int.TryParse(target, out _))
                    return Convert.ToInt32(target);
                else if (double.TryParse(target, out _))
                    return Convert.ToDouble(target);
                else if (target == "true")
                    return true;
                else if (target == "false")
                    return false;
                else if (target == "null")
                    return null;
                else if (!String.IsNullOrWhiteSpace(target))
                    throw new JsonParserException(lineCounter);
                listCounter++;
            }
            throw new JsonParserException(lineCounter);
        }

        private static string ParseString(ref string[] jsonList, ref int lineCounter, ref int listCounter)
        {
            StringBuilder sb = new StringBuilder();
            listCounter++;
            while (listCounter < jsonList.Length)
            {
                string target = jsonList[listCounter];
                if (target == "\"" && jsonList[listCounter - 1] != "\\")
                {
                    listCounter++;
                    return sb.ToString();
                }
                if (target == "\n")
                    lineCounter++;
                sb.Append(target);
                listCounter++;
            }
            throw new JsonParserException(lineCounter);
        }
    }
}
