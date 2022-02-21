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
    }
}