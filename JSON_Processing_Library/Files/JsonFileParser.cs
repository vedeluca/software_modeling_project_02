using JsonProcessing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Files
{
    public class JsonFileParser : IDataFileParser
    {
        /// <summary>
        /// Parse the JSON file at path and 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The root DataNode</returns>
        public DataNode ParseDataFile(string path)
        {
            string text = File.ReadAllText(path);
            return new DataStringParser().ParseDataString(text);
        }
    }
}
