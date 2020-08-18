using System;
using System.Collections.Generic;

namespace expertsystem.models
{
    /// <summary>
    /// Node object
    /// </summary>
    public class Node
    {
        private readonly List<Edge> _edges = new List<Edge>();
        private readonly string _content;

        public Node(string content) => _content = content;

        /// <summary>
        /// Add edge to edges list
        /// </summary>
        /// <param name="edge">Edge to add</param>
        public void AddEdge(Edge edge) => _edges.Add(edge);

        /// <summary>
        /// Get edges
        /// </summary>
        /// <returns>List of edges</returns>
        public List<Edge> GetEdges() => _edges;

        /// <summary>
        /// Get node content
        /// </summary>
        /// <returns>Content</returns>
        public string GetContent() => _content;

        /// <summary>
        /// Returns true if node has no edges
        /// </summary>
        /// <returns>true if node has no edges otherwise false</returns>
        public bool HasNoEdges()
        {
            return CountEdges() == 0;
        }

        /// <summary>
        /// Count amount of edges
        /// </summary>
        /// <returns>Amount of edges</returns>
        private int CountEdges() => _edges.Count;

        /// <summary>
        /// Print connections of node to console
        /// </summary>
        /// <param name="indent">indentation for leaf</param>
        /// <param name="last">is last leaf</param>
        public void PrintTree(string indent, bool last)
        {
            Console.Write(indent);

            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }

            Console.WriteLine(GetContent());

            for (var i = 0; i < CountEdges(); i++)
            {
                GetEdges()[i].PrintTree(indent, i == CountEdges() - 1);
            }
        }
    }
}