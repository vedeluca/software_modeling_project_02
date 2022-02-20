using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO: may need to rethink where this goes
namespace JsonProcessing
{
    public class JsonException : Exception
    {
        public JsonException() { }

        /// <summary>
        /// This exception will point out the line in the JSON string causing the problem
        /// </summary>
        /// <param name="line"></param>
        public JsonException(int line) : base(String.Format("Invalid JSON Format on line {0}", line)) { }
    }
}
