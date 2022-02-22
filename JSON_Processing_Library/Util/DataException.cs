﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Util
{
    public class DataException : Exception
    {
        public DataException() { }

        /// <summary>
        /// This exception will point out the line in the data string causing the problem
        /// </summary>
        /// <param name="line"></param>
        public DataException(int line) : base(String.Format("Invalid format on line {0}", line)) { }
    }
}
