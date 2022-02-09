using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace JsonProcessing
{
    public static class JsonParser
    {
        /// <summary>
        /// Converts a JSON string to a JsonNode.
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns cref="IJsonNode">This JsonNode is the root of the JSON string</returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="JsonParserException"></exception>
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
        }

        /// <summary>
        /// Looks through the JSON list at the current position to add keys and values to the JsonObject
        /// </summary>
        /// <param name="jsonObject" cref="JsonObject{TKey, TValue}"></param>
        /// <param name="jsonList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <returns cref="JsonObject{TKey, TValue}">Returns the jsonObject this method extends</returns>
        /// <exception cref="JsonParserException"></exception>
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

        /// <summary>
        /// Looks through the JSON list at the current position to add values to the JsonArray
        /// </summary>
        /// <param name="jsonArray" cref="JsonArray{T}"></param>
        /// <param name="jsonList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <returns cref="JsonArray{T}">Returns the JsonArray this method extends</returns>
        /// <exception cref="JsonParserException"></exception>
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

        /// <summary>
        /// Looks througn the JSON list at the current position to read the value of the current key
        /// </summary>
        /// <param name="jsonObject" cref="JsonObject{TKey, TValue}"></param>
        /// <param name="jsonList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <returns>After finding the colon, returns the value from ParseValueAndEnd</returns>
        /// <see cref="ParseValueAndEnd(IJsonNode, string, ref string[], ref int, ref int)"/>
        /// <exception cref="JsonParserException"></exception>
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

        /// <summary>
        /// Looks througn the JSON list at the current position to read the current value and check if the entry ends correctly
        /// </summary>
        /// <param name="parent" cref="IJsonNode"></param>
        /// <param name="end"></param>
        /// <param name="jsonList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <returns>When this method finds a comma or the character from the "end" parameter, it returns the value from ParseValue</returns>
        /// <see cref="ParseValue(IJsonNode, ref string[], ref int, ref int)"/>
        /// <exception cref="JsonParserException"></exception>
        private static object? ParseValueAndEnd(IJsonNode parent, string end, ref string[] jsonList, ref int lineCounter, ref int listCounter)
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

        /// <summary>
        /// Looks througn the JSON list at the current position to read the current value
        /// </summary>
        /// <param name="parent" cref="IJsonNode"></param>
        /// <param name="jsonList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <returns>Returns a JsonObject, JsonArray, string, int, double, bool, or null</returns>
        /// <exception cref="JsonParserException"></exception>
        private static object? ParseValue(IJsonNode parent, ref string[] jsonList, ref int lineCounter, ref int listCounter)
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

        /// <summary>
        /// If a string is detected in the JSON list, this method looks for the end
        /// </summary>
        /// <param name="jsonList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <returns>Returns the whole string without the quotation marks at the ends</returns>
        /// <exception cref="JsonParserException"></exception>
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
