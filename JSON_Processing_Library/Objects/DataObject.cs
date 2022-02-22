using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class DataObject
    {
        private IDataObject dataObject;
        public DataObject? Parent { get; set; }
        public DataObject? Root { get; set; }

        public DataObject(IDataObject obj) : base()
        {
            dataObject = obj;
        }

        public DataObject(IDataObject obj, DataObject parent) : this(obj)
        {
            Parent = parent;
            Root = parent.Root ?? parent;
        }
        public void Add(string key, dynamic? value, int line)
        {
            dataObject.Add(key, value, line);
        }
        public override string ToString()
        {
            return dataObject.ToString("");
        }
        public string ToString(string tabs)
        {
            return dataObject.ToString(tabs);
        }
        public DataValue Query(string search)
        {
            return dataObject.Query(search);
        }
    }
}
