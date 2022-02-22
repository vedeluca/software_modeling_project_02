using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public interface IDataNodeParser
    {
        DataNode ParseDataNode(DataNode node, ref string[] stringList, ref int lineCounter, ref int listCounter);
    }
}
