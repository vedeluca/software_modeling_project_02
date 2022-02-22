using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonProcessing.Files;
using JsonProcessing.Objects;
using JsonProcessing.Util;
using JsonProcessing.Values;

namespace JsonTesting
{
    [TestClass]
    public class JsonTest
    {
        private string jsonString;
        private string brokenString;

        [TestInitialize]
        public void JsonTestInit()
        {
            StringBuilder sb = new();
            sb.Append("{\n");//1
            sb.Append("\t\"glossary\": {\n");//2
            sb.Append("\t\t\"title\": \"example glossary\",\n");//3
            sb.Append("\t\t\"GlossDiv\": {\n");//4
            sb.Append("\t\t\t\"title\": \"S\",\n");//5
            sb.Append("\t\t\t\"GlossList\": {\n");//6
            sb.Append("\t\t\t\t\"GlossEntry\": {\n");//7
            sb.Append("\t\t\t\t\t\"ID\": 123,\n");//8
            sb.Append("\t\t\t\t\t\"SortAs\": true,\n");//9
            sb.Append("\t\t\t\t\t\"GlossTerm\": \"Standard Generalized Markup Language\",\n");//10
            sb.Append("\t\t\t\t\t\"Acronym\": -4.56,\n");//11
            sb.Append("\t\t\t\t\t\"Abbrev\": \"ISO 8879:1986\",\n");//12
            sb.Append("\t\t\t\t\t\"GlossDef\": {\n");//13
            sb.Append("\t\t\t\t\t\t\"para\": \"A meta-markup language, used to create markup languages such as DocBook.\",\n");//14
            sb.Append("\t\t\t\t\t\t\"GlossSeeAlso\": [\n");//15
            sb.Append("\t\t\t\t\t\t\t\"GML\",\n");//16
            sb.Append("\t\t\t\t\t\t\t\"XML\"\n");//17
            sb.Append("\t\t\t\t\t\t]\n");//18
            sb.Append("\t\t\t\t\t},\n");//16
            sb.Append("\t\t\t\t\t\"GlossSee\": null\n");//17
            sb.Append("\t\t\t\t}\n");//18
            sb.Append("\t\t\t}\n");//19
            sb.Append("\t\t}\n");//20
            sb.Append("\t}\n");//21
            sb.Append("}");//22
            jsonString = sb.ToString();

            StringBuilder broken = new();
            broken.Append("{\n");//1
            broken.Append("\t\"glossary\": {\n");//2
            broken.Append("\t\t\"title\": \"example glossary\",\n");//3
            broken.Append("\t\t\"GlossDiv\": {\n");//4
            broken.Append("\t\t\t\"title\": \"S\",\n");//5
            broken.Append("\t\t\t\"GlossList\": {\n");//6
            broken.Append("\t\t\t\t\"GlossEntry\": {\n");//7
            broken.Append("\t\t\t\t\t\"ID\": 123,\n");//8
            broken.Append("\t\t\t\t\t\"SortAs\": true,\n");//9
            broken.Append("\t\t\t\t\t\"GlossTerm\": \"Standard Generalized Markup Language\",\n");//10
            broken.Append("\t\t\t\t\t\"Acronym\": -4.56,\n");//11
            broken.Append("\t\t\t\t\t\"Abbrev\": \"ISO 8879:1986\",\n");//12
            broken.Append("\t\t\t\t\t\"GlossDef\": {\n");//13
            broken.Append("\t\t\t\t\t\t\"para\": \"A meta-markup language, used to create markup languages such as DocBook.\",\n");//14
            broken.Append("\t\t\t\t\t\t\"GlossSeeAlso\": [\n");//15
            broken.Append("\t\t\t\t\t\t\t\"GML\",\n");//16
            broken.Append("\t\t\t\t\t\t\t\"XML\"\n");//17
            broken.Append("\t\t\t\t\t\t}\n");//18............This is where it should break
            broken.Append("\t\t\t\t\t},\n");//16
            broken.Append("\t\t\t\t\t\"GlossSee\": null\n");//17
            broken.Append("\t\t\t\t}\n");//18
            broken.Append("\t\t\t}\n");//19
            broken.Append("\t\t}\n");//20
            broken.Append("\t}\n");//21
            broken.Append("}");//22
            brokenString = broken.ToString();
        }

        [TestMethod]
        public void TransformStringToObject()
        {
            DataStringParser parser = new(new JsonStringParser());
            Assert.IsNotNull(parser, "Parser is null");
            DataNode root = parser.ParseDataString(jsonString);
            Assert.IsNotNull(root, "Root node is null");
            string json = root.ToString();
            Assert.AreEqual(json, jsonString, "Output string does not match input string");
            Console.WriteLine(json);
        }

        [TestMethod]
        public void ThrowDataParserException()
        {
            DataStringParser parser = new(new JsonStringParser());
            Assert.IsNotNull(parser, "Parser is null");
            Assert.ThrowsException<DataParserException>(() => parser.ParseDataString(brokenString), "Exception expected for broken JSON string");
            try
            {
                parser.ParseDataString(brokenString);
            }
            catch (DataParserException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [TestMethod]
        public void AddJsonObject()
        {
            DataStringParser parser = new(new JsonStringParser());
            Assert.IsNotNull(parser, "Parser is null");
            DataNode root = parser.ParseDataString(jsonString);
            Assert.IsNotNull(root, "Root node is null");
            Assert.AreEqual(root.Type, DataType.Object, "Root node is not an object");
            DataNode node = new(new JsonObject(), DataType.Object);
            node.Add("Boolean Test", new DataValue(new JsonValue(false, 0)));
            node.Add("Number Test", new DataValue(new JsonValue(-123.45, 0)));
            node.Add("String Test", new DataValue(new JsonValue("test \\\"test\\\"", 0)));
            root.Add("node", new DataValue(new JsonValue(node, 0)));
            /*object? val = rootObj["node"];
            Assert.IsNotNull(val, "Value in root is null");
            Assert.IsInstanceOfType(val, typeof(JsonObject<string, object?>), "Value in root is not JsonObject type");
            JsonObject<string, object?> valObj = (JsonObject<string, object?>)val;
            Assert.AreEqual(valObj["Boolean Test"], node["Boolean Test"], "Objects do not have matching values");
            Assert.AreEqual(valObj["Number Test"], node["Number Test"], "Objects do not have matching values");
            Assert.AreEqual(valObj["String Test"], node["String Test"], "Objects do not have matching values");
            Console.WriteLine(rootObj.ToString());*/
        }

        [TestMethod]
        public void QueryJsonArray()
        {
            DataStringParser parser = new(new JsonStringParser());
            Assert.IsNotNull(parser, "Parser is null");
            DataNode root = parser.ParseDataString(jsonString);
            Assert.IsNotNull(root, "Root node is null");
            DataValue query = root.Query("GlossSeeAlso");
            Assert.IsNotNull(query, "Query is null");
            Assert.IsInstanceOfType(query.GetValue(), typeof(DataNode), "Queried object is not a DataNode");
            Assert.AreEqual(query.Type, DataType.Array, "Queried objectis not a JsonArray");
            DataNode queryArr = (DataNode)query.GetValue();
            Console.WriteLine(queryArr.ToString());
        }
    }
}