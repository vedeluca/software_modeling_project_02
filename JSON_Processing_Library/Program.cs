// See https://aka.ms/new-console-template for more information
using JsonProcessing;

string JsonString =
"{\n" +
"    \"glossary\": {\n" +
"    \"title\": \"example glossary\",\n" +
"		\"GlossDiv\": {\n" +
"        \"title\": \"S\",\n" +
"			\"GlossList\": {\n" +
"            \"GlossEntry\": {\n" +
"                \"ID\": \"SGML\",\n" +
"					\"SortAs\": \"SGML\",\n" +
"					\"GlossTerm\": \"Standard Generalized Markup Language\",\n" +
"					\"Acronym\": \"SGML\",\n" +
"					\"Abbrev\": \"ISO 8879:1986\",\n" +
"					\"GlossDef \": {\n" +
"                    \"para\": \"A meta-markup language, used to create markup languages such as DocBook.\",\n" +
"						\"GlossSeeAlso\": [\"GML\", \"XML\"]\n" +
"                    },\n" +
"					\"GlossSee\": null\n" +
"                }\n" +
"        }\n" +
"    }\n" +
"}\n" +
"}";
JsonParser parser = new JsonParser();
JsonNode test = parser.StringToJsonNode(JsonString);
//string query = test.QueryToString("GlossSeeAlso");
Console.WriteLine(test.ToString());