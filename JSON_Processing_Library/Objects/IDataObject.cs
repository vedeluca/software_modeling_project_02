using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public interface IDataObject
    {
        void Add(string key, dynamic? value, int line);
        string ToString(string tabs);
        DataValue Query(string search);
    }
}
