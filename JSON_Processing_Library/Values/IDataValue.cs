using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public interface IDataValue
    {
        DataType Type { get; set; }
        dynamic GetValue();
        string ToString(string tabs);
    }
}
