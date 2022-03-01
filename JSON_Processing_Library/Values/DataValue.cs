using JsonProcessing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public class DataValue
    {
        private IDataValue dataValue;
        public DataType Type
        {
            get
            {
                return dataValue.Type;
            }
        }

        public DataValue()
        {
            dataValue = new JsonValue();
        }
        public DataValue(IDataValue value)
        {
            dataValue = value;
        }
        public object GetValue()
        {
            return dataValue.GetValue();
        }
        public override string ToString()
        {
            return dataValue.ToString("");
        }

        public string ToString(string tabs)
        {
            return dataValue.ToString(tabs);
        }
    }
}
