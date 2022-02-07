using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing
{
    internal interface JsonNode
    {
        JsonNode Parent { get; set; }
        JsonNode Root { get; set; }

        string ToString();

        object Query(string search);
    }
}
