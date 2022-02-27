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
        public int Count
        {
            get
            {
                return Node.Count;
            }
        }

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
        public DataValue GetValueAt(string key)
        {
            return Node.GetValueAt(key);
        }
        public DataValue GetValueAt(int index)
        {
            return Node.GetValueAt(index);
        }
        public string GetKeyAt(int index)
        {
            return Node.GetKeyAt(index);
        }
    }
}
