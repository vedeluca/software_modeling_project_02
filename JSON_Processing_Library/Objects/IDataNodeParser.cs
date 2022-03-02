using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing.Objects
{
    public interface IDataNodeParser
    {
        /// <summary>
        /// Parse the current DataNode
        /// </summary>
        /// <param name="node"></param>
        /// <param name="stringList"></param>
        /// <param name="lineCounter"></param>
        /// <param name="listCounter"></param>
        /// <returns>A new DataNode</returns>
        DataNode ParseDataNode(DataNode node, ref string[] stringList, ref int lineCounter, ref int listCounter);
    }
}
