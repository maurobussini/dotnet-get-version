using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZenProgramming.DotNetGetVersion.Cli
{
    /// <summary>
    /// Utilities for .csproj C# files
    /// </summary>
    public static class CsprojUtils
    {
        /// <summary>
        /// Filters list of XML nodes with tag "PropertyGroup"
        /// </summary>
        /// <param name="nodes">Source nodes</param>
        /// <returns>Returns list of nodes</returns>
        public static IList<XmlNode> FilterPropertyGroupNodes(XmlNodeList nodes) 
        {
            //Arguments validation
            if (nodes == null) throw new ArgumentNullException(nameof(nodes));

            //List of out nodes
            IList<XmlNode> result = new List<XmlNode>();

            //Iterate nodes
            foreach (XmlNode current in nodes) 
            {
                //If curreny node is "PropertyGroup"
                if (current.Name == "PropertyGroup") 
                {
                    //Add to list
                    Debug.WriteLine($"Found 'PropertyGroup' XML tag...");
                    result.Add(current);
                }
                    
            }

            //Returns list of empty
            return result;
        }

        /// <summary>
        /// Check if provided list of nodes contains "Version"
        /// </summary>
        /// <param name="childNodes">List of nodes</param>
        /// <returns>Returns node with version or null</returns>
        public static XmlNode GetFirstVersionNode(XmlNodeList childNodes)
        {
            //Arguments validation
            if (childNodes == null) throw new ArgumentNullException(nameof(childNodes));

            //Iterate nodes
            foreach (XmlNode current in childNodes)
            {
                //Procedo finchè non trovo quello "Version"
                if (current.Name != "Version")
                    continue;

                //If found, return
                Debug.WriteLine($"Found 'Version' XML tag...");
                return current;                
            }

            //Return not found
            return null;
        }
    }
}
