// See https://aka.ms/new-console-template for more information
using JsonProcessing;

string JsonString =
"{\n" +//1
"    \"glossary\": {\n" +//2
"    \"title\": \"example glossary\",\n" +//3
"		\"GlossDiv\": {\n" +//4
"        \"title\": \"S\",\n" +//5
"			\"GlossList\": {\n" +//6
"            \"GlossEntry\": {\n" +//7
"                \"ID\": \"SGML\",\n" +//8
"					\"SortAs\": \"SGML\",\n" +//9
"					\"GlossTerm\": \"Standard Generalized Markup Language\",\n" +//10
"					\"Acronym\": \"SGML\",\n" +//11
"					\"Abbrev\": \"ISO 8879:1986\",\n" +//12
"					\"GlossDef\": {\n" +//13
"                    \"para\": \"A meta-markup language, used to create markup languages such as DocBook.\",\n" +//14
"						\"GlossSeeAlso\": [\"GML\", \"XML\"]\n" +//15
"                    },\n" +//16
"					\"GlossSee\": null\n" +//17
"                }\n" +//18
"            }\n" +//19
"        }\n" +//20
"    }\n" +//21
"}";//22
try
{
    IJsonNode root = JsonParser.StringToJsonNode(JsonString);
    object obj = root.Query("GlossDef");
    Console.WriteLine(root.QueryToString("GlossDef"));
    if (obj is JsonObject<string, object?>)
    {
        JsonObject<string, object?> node = (JsonObject<string, object?>)obj;
        node.Add("Boolean Test", false);
        node.Add("Number Test", -123.45);
        node.Add("String Test", "test \\\"test\\\"");
    }

    Console.WriteLine(root.ToString());
}
catch (Exception e) when (e is JsonParserException || e is NotImplementedException || e is NullReferenceException)
{
    Console.WriteLine(e);
}
