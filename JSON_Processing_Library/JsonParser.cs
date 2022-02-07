using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace JsonProcessing
{
    internal class JsonParser
    {
        private string[] JsonList { get; set; }
        private int LineCounter { get; set; }
        private int ListCounter { get; set; }

        public JsonParser()
        {

        }

        public JsonNode? StringToJsonNode(string jsonString)
        {
            JsonList = Regex.Split(jsonString, @"({|}|\[|\]|,|:|""|\\|\n|null|true|false)");
            if (JsonList == null)
                throw new NullReferenceException();
            LineCounter = 1;
            ListCounter = 0;
            while (ListCounter < JsonList.Length)
            {
                string target = JsonList[ListCounter];
                if (target == "\n")
                {
                    LineCounter++;
                }
                else if (target == "{")
                {
                    ListCounter++;
                    return ParseJsonObject(new JsonObject<string, object?>());
                }
                else if (target == "[")
                {
                    ListCounter++;
                    return ParseJsonArray(new JsonArray<object?>());
                }
                else if (!String.IsNullOrWhiteSpace(target))
                {
                    throw new JsonParserException(LineCounter);
                }
                ListCounter++;
            }
            return null;
        }

        private JsonObject<string, object?> ParseJsonObject(JsonObject<string, object?> jsonObject)
        {
            while (ListCounter < JsonList.Length)
            {
                string target = JsonList[ListCounter];
                if (target == "\n")
                {
                    LineCounter++;
                }
                else if (target == "\"")
                {
                    ListCounter++;
                    string? key = ParseString();
                    if (key == null)
                        throw new JsonParserException(LineCounter);
                    ListCounter++;
                    object? value = null;
                    bool loop = true;
                    while (ListCounter < JsonList.Length && loop)
                    {
                        string subtarget = JsonList[ListCounter];
                        if (subtarget == "\n")
                        {
                            LineCounter++;
                        }
                        else if (subtarget == ":")
                        {
                            ListCounter++;
                            value = ParseValue(jsonObject, "}");
                            loop = false;
                        }
                        else if (!String.IsNullOrWhiteSpace(subtarget))
                        {
                            throw new JsonParserException(LineCounter);
                        }
                        ListCounter++;
                    }
                    if (loop)
                        throw new JsonParserException(LineCounter);
                    jsonObject.Add(key, value);
                    if (JsonList[ListCounter - 1] == "}")
                    {
                        return jsonObject;
                    }
                }
                else if (!String.IsNullOrWhiteSpace(target))
                {
                    throw new JsonParserException(LineCounter);
                }
                ListCounter++;

            }
            throw new JsonParserException(LineCounter);
        }

        private JsonNode ParseJsonArray(JsonArray<object?> jsonArray)
        {
            Type type = null;
            while (ListCounter < JsonList.Length)
            {
                string target = JsonList[ListCounter];
                if (target == "\n")
                {
                    LineCounter++;
                }
                else if (!String.IsNullOrWhiteSpace(target))
                {
                    object? value = ParseValue(jsonArray, "]");
                    if (value == null && type != null)
                        throw new JsonParserException(LineCounter);
                    else if (value != null)
                    {
                        if (type == null)
                            type = value.GetType();
                        else if (!type.Equals(value.GetType()))
                            throw new JsonParserException(LineCounter);
                    }
                    jsonArray.Add(value);
                    if (JsonList[ListCounter] == "]")
                    {
                        return jsonArray;
                    }
                }
                ListCounter++;

            }
            throw new JsonParserException(LineCounter);
        }

        private object? ParseValue(JsonNode parent, string end)
        {
            object? value = null;
            bool checkEnd = false;
            while (ListCounter < JsonList.Length)
            {
                string target = JsonList[ListCounter];
                if (target == "\n")
                {
                    LineCounter++;
                }
                else if (target == "{")
                {
                    if (checkEnd)
                        throw new JsonParserException(LineCounter);
                    ListCounter++;
                    value = ParseJsonObject(new JsonObject<string, object?>(parent));
                    if (value == null)
                        throw new JsonParserException(LineCounter);
                    checkEnd = true;
                }
                else if (target == "[")
                {
                    if (checkEnd)
                        throw new JsonParserException(LineCounter);
                    ListCounter++;
                    value = ParseJsonArray(new JsonArray<object?>(parent));
                    if (value == null)
                        throw new JsonParserException(LineCounter);
                    checkEnd = true;
                }
                else if (target == "\"")
                {
                    if (checkEnd)
                        throw new JsonParserException(LineCounter);
                    ListCounter++;
                    value = ParseString();
                    if (value == null)
                        throw new JsonParserException(LineCounter);
                    checkEnd = true;
                }
                else if (double.TryParse(target, out _))
                {
                    if (checkEnd)
                        throw new JsonParserException(LineCounter);
                    value = Convert.ToDouble(target);
                    checkEnd = true;
                }
                else if (int.TryParse(target, out _))
                {
                    if (checkEnd)
                        throw new JsonParserException(LineCounter);
                    value = Convert.ToInt32(target);
                    checkEnd = true;
                }
                else if (target == "true")
                {
                    if (checkEnd)
                        throw new JsonParserException(LineCounter);
                    value = true;
                    checkEnd = true;
                }
                else if (target == "false")
                {
                    if (checkEnd)
                        throw new JsonParserException(LineCounter);
                    value = false;
                    checkEnd = true;
                }
                else if (target == "null")
                {
                    if (checkEnd)
                        throw new JsonParserException(LineCounter);
                    value = null;
                    checkEnd = true;
                }
                else if (target == "," || target == end)
                {
                    if (!checkEnd)
                        throw new JsonParserException(LineCounter);
                    return value;
                }
                else if (!String.IsNullOrWhiteSpace(target))
                {
                    throw new JsonParserException(LineCounter);
                }
                ListCounter++;
            }
            return null;
        }

        private string? ParseString()
        {
            StringBuilder sb = new StringBuilder();
            while (ListCounter < JsonList.Length)
            {
                string target = JsonList[ListCounter];
                if (target == "\"" && JsonList[ListCounter - 1] != "\\")
                    return sb.ToString();
                if (target == "\n")
                    LineCounter++;
                sb.Append(target);
                ListCounter++;
            }
            return null;
        }
    }
}
