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
        DataNode ParseDataString(string dataString);
    }
}
