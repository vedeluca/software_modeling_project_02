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
        private IDataNode dataNode;
        public DataType Type { get; set; }
        public DataNode? Parent { get; set; }
        public DataNode? Root { get; set; }

        public DataNode(IDataNode node, DataType type) : base()
        {
            dataNode = node;
            Type = type;
        }

        public DataNode(IDataNode node, DataType type, DataNode parent) : this(node, type)
        {
            Parent = parent;
            Root = parent.Root ?? parent;
        }
        public void Add(string key, DataValue value)
        {
            dataNode.Add(key, value);
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
