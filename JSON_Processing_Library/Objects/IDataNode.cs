using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public interface IDataNode
    {
        void Add(string key, DataValue value);
        string ToString(string tabs);
        DataValue Query(string search);
        DataValue Get(string key);
        DataValue Get(int index);
    }
}
