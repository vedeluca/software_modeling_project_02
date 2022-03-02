using JsonProcessing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Files
{
    public interface IDataStringParser
    {
        /// <summary>
        /// Convert JSON string to DataNode
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns>The root DataNode</returns>
        DataNode ParseDataString(string dataString);
    }
}
