using System;

namespace expertsystem.models
{
    /// <summary>
    /// Edge object
    /// </summary>
    public class Edge
    {
        private readonly string _content;
        private Node _nextNode;

        public Edge(string content) => _content = content;

        /// <summary>
        /// Set node
        /// </summary>
        /// <param name="node">Node</param>
        public void SetNode(Node node) => _nextNode = node;

        /// <summary>
        /// Get Node
        /// </summary>
        /// <returns>Node</returns>
        public Node GetNode() => _nextNode;

        /// <summary>
        /// Get content of edge
        /// </summary>
        /// <returns>Content</returns>
        public string GetContent() => _content;

        /// <summary>
        /// Print connections of edge to console
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

            GetNode().PrintTree(indent, GetNode().HasNoEdges());
        }
    }
}