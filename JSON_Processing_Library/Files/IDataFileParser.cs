using JsonProcessing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Files
{
    public interface IDataFileParser
    {
        /// <summary>
        /// Parse the JSON file at path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The root DataNode</returns>
        DataNode ParseDataFile(string path);
    }
}
