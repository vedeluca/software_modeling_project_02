using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing
{
    public class JsonNode
    {
        public JsonObject? JsonObjectParent { get; set; }
        public JsonObject? JsonObjectChild { get; set; }
        public JsonObject? JsonObjectRoot { get; set; }
        public JsonArray? JsonArrayParent { get; set; }
        public JsonArray? JsonArrayChild { get; set; }
        public JsonArray? JsonArrayRoot { get; set; }

        public JsonNode()
        {

        }

        public JsonNode(JsonNode parent)
        {
            JsonObjectParent = parent.JsonObjectChild;
            JsonObjectRoot = parent.JsonObjectRoot;
            JsonArrayParent = parent.JsonArrayChild;
            JsonArrayRoot = parent.JsonArrayRoot;
        }

    }
}