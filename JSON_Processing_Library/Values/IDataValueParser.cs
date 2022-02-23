using JsonProcessing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public interface IDataValueParser
    {
        DataValue ParseDataValue(DataNode parent, ref string[] stringList, ref int lineCounter, ref int listCounter, string end);
        string ParseString(ref string[] stringList, ref int lineCounter, ref int listCounter);
    }
}
