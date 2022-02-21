using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class DataNode : DataObject
    {
        public DataNode NodeParent { get; set; }
        public DataNode NodeRoot { get; set; }
        public bool IsRoot { get; set; }

        public DataNode()
        {
            NodeParent = new DataNode();
            NodeRoot = new DataNode();
            IsRoot = true;
        }

        public DataNode(DataNode parent) : this()
        {
            NodeParent = parent;
            NodeRoot = (parent.IsRoot) ? parent : parent.NodeRoot;
            IsRoot = false;
        }

        public override void Add(string key, dynamic? value, int line)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override string ToString(string tabs)
        {
            throw new NotImplementedException();
        }

        public override DataValue Query(string search)
        {
            throw new NotImplementedException();
        }
    }
}