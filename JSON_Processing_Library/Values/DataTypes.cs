using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public enum EmptyType
    {
        Empty
    }
    public enum JsonType
    {
        Empty,
        String,
        Integer,
        Double,
        Boolean,
        Array,
        Object,
        Null
    }
}
