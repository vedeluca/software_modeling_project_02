using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public abstract class DataObject
    {
        protected DataNode node;
        public DataObject()
        {
            node = new DataNode();
        }
        public DataObject(DataNode parent) : this()
        {
            node = new DataNode(parent);
        }
        public abstract void Add(string key, dynamic? value, int line);
        new public abstract string ToString();
        public abstract string ToString(string tabs);
        public abstract DataValue Query(string search);
    }
}
