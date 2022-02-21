using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Values
{
    public abstract class DataValue
    {
        protected Enum type;
        public DataValue()
        {
            type = EmptyType.Empty;
        }
        protected abstract string MoreTabs(string tabs);
        public abstract dynamic GetValue();
        new public abstract Enum GetType();
        new public abstract string ToString();
        public abstract string ToString(string tabs);
    }
}
