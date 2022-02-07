using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing
{
    internal class JsonParserException : Exception
    {
        public JsonParserException() { }
        public JsonParserException(int line) : base(String.Format("Invalid JSON Format on line {0}", line)) { }
    }
}
