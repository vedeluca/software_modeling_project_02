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
        /// This exception will point out the line in the data string causing the problem
        /// </summary>
        /// <param name="line"></param>
        public DataParserException(int line) : base(String.Format("Invalid format on line {0}", line)) { }
        public DataParserException(string type) : base(String.Format("Invalid format found in {0}", type)) { }
    }
}
