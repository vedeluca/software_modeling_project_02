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
        int Count { get; }
        void Add(string key, DataValue value);
        string ToString(string tabs);
        DataValue Query(string search);
        DataValue GetValueAt(string key);
        DataValue GetValueAt(int index);
        string GetKeyAt(int index);
    }
}
