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
        public DataNode ParseDataFile(string path)
        {
            string text = File.ReadAllText(path);
            return new DataStringParser(new JsonStringParser()).ParseDataString(text);
        }
    }
}
