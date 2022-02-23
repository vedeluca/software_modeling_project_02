using JsonProcessing.Values;
using JsonProcessing.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class DataNode
    {
        public IDataNode Node { get; set; }
        public DataNode? Parent { get; set; }
        public DataNode? Root { get; set; }

        public DataNode(IDataNode node) : base()
        {
            Node = node;
        }

        public DataNode(IDataNode node, DataNode parent) : this(node)
        {
            Parent = parent;
            Root = parent.Root ?? parent;
        }
        public void Add(string key, DataValue value)
        {
            Node.Add(key, value);
        }

        public void Add(DataValue value)
        {
            Node.Add("", value);
        }
        public override string ToString()
        {
            return Node.ToString("");
        }
        public string ToString(string tabs)
        {
            return Node.ToString(tabs);
        }
        public DataValue Query(string search)
        {
            return Node.Query(search);
        }
        public DataValue Get(string key)
        {
            return Node.Get(key);
        }
        public DataValue Get(int index)
        {
            return Node.Get(index);
        }
    }
}
