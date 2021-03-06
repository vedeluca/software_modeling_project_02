using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Util
{
    public class DataParserException : Exception
    {
        public DataParserException() { }

        /// <summary>
        /// This exception will point out the line in the data string causing the problem,
        /// as well as the data type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="line"></param>
        public DataParserException(string type = "unknown", int line = 0) : base(String.Format("Invalid {0} on line {1}", type, line)) { }
    }
    public class DataParserTypeException : Exception
    {
        /// <summary>
        /// This exception will point out the value type causing the exception and return the
        /// type for DataParserException to use
        /// </summary>
        public DataType Type { get; set; }
        public DataParserTypeException(DataType type) : base(String.Format("Invalid new {0}", type.ToString()))
        {
            Type = type;
        }
    }
    public class DataParserLineException : Exception
    {
        /// <summary>
        /// This exception will point out the line in the data string causing the problem
        /// </summary>
        /// <param name="line"></param>
        public DataParserLineException(int line) : base(String.Format("Invalid format on line {0}", line)) { }
    }
}
