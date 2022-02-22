using JsonProcessing.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public class DataNode
    {
        private IDataNode dataNode;
        public DataNode? Parent { get; set; }
        public DataNode? Root { get; set; }

        public DataNode(IDataNode node) : base()
        {
            dataNode = node;
        }

        public DataNode(IDataNode node, DataNode parent) : this(node)
        {
            Parent = parent;
            Root = parent.Root ?? parent;
        }
        public void Add(string key, dynamic? value, int line)
        {
            dataNode.Add(key, value, line);
        }
        public override string ToString()
        {
            return dataNode.ToString("");
        }
        public string ToString(string tabs)
        {
            return dataNode.ToString(tabs);
        }
        public DataValue Query(string search)
        {
            return dataNode.Query(search);
        }
    }
}
