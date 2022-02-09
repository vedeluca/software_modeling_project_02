using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonProcessing
{
    public interface IJsonNode
    {
        /// <summary>
        /// The direct parent of the current node
        /// </summary>
        IJsonNode? Parent { get; set; }

        /// <summary>
        /// The root of the entire JSON tree
        /// </summary>
        IJsonNode? Root { get; set; }

        /// <summary>
        /// Replaces the ToString method
        /// </summary>
        /// <returns>The entire node in string format, with proper indentation</returns>
        string ToString();

        /// <summary>
        /// Keeps track of indentation when converting nested JsonNodes to string
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns>The entire node in string format, with proper indentation</returns>
        string ToString(string tabs);

        /// <summary>
        /// Searches the node and all of its children for a value with the key of "search"
        /// </summary>
        /// <param name="search"></param>
        /// <returns>The object being searched, or null</returns>
        object? Query(string search);
    }
}
