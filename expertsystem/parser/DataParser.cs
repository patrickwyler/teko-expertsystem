using System;
using System.Collections.Generic;
using System.Linq;
using expertsystem.models;
using expertsystem.services;
using static System.String;

namespace expertsystem.parser
{
    /// <summary>
    /// Class for parsing data from our txt files
    /// </summary>
    public class DataParser
    {
        // Common constants
        private const int SpacePosition = 1;
        private const string RuleIdentifier = "Wenn";
        private const string RuleEdgeIdentifier = "dann";
        private const string Space = " ";

        // Data file paths
        private const string MapdataFilePath = "../mapdata.txt";
        private const string PossibleEdgesFilePath = "../possibleEdges.txt";

        // Variables
        private InputService InputService { get; } = new InputService();
        private IEnumerable<string> PossibleEdges { get; set; } = new List<string>();

        public List<Node> Parse()
        {
            var nodes = new List<Node>();

            ParsePossibleEdges();
            ParseNodesAndEdges(nodes);
            ParseRules(nodes);

            return nodes;
        }

        /// <summary>
        /// Parse possible edges from file
        /// </summary>
        private void ParsePossibleEdges()
        {
            // Longest edge word should be on the top of the list because we're trying to match them
            // later before the shorter ones
            //
            // Example: "ist nicht" should match before "ist"
            PossibleEdges = InputService.GetRows(PossibleEdgesFilePath).OrderByDescending(x => x.Length);
        }

        /// <summary>
        /// Parse nodes and edges from file and add them to the nodes list
        /// </summary>
        /// <param name="nodes">List of nodes to modify</param>
        private void ParseNodesAndEdges(List<Node> nodes)
        {
            var nodeLines = InputService.GetRows(MapdataFilePath).FindAll(x => !x.Contains(RuleIdentifier));
            foreach (var line in nodeLines)
            {
                foreach (var edgeWord in PossibleEdges)
                {
                    var position = line.IndexOf(edgeWord, StringComparison.Ordinal);
                    if (position != -1)
                    {
                        var newSourceNodeName = line.Substring(0, position - SpacePosition);
                        var newTargetNodeName = line.Substring(position + SpacePosition + edgeWord.Length);

                        var newSourceNodeToAdd = new Node(newSourceNodeName);
                        var newEdgeToAdd = new Edge(edgeWord);
                        var newTargetNodeToAdd = new Node(newTargetNodeName);

                        foreach (var node in nodes)
                        {
                            if (node.GetContent().Equals(newSourceNodeName))
                            {
                                node.AddEdge(newEdgeToAdd);
                                newSourceNodeToAdd = node;
                            }
                        }

                        if (newSourceNodeToAdd.HasNoEdges())
                        {
                            newSourceNodeToAdd.AddEdge(newEdgeToAdd);
                        }

                        if (!nodes.Contains(newSourceNodeToAdd))
                        {
                            nodes.Add(newSourceNodeToAdd);
                        }

                        foreach (var node in nodes)
                        {
                            if (node.GetContent().Equals(newTargetNodeName))
                            {
                                newEdgeToAdd.SetNode(node);
                                newTargetNodeToAdd = node;
                            }
                        }

                        if (!nodes.Contains(newTargetNodeToAdd))
                        {
                            newEdgeToAdd.SetNode(newTargetNodeToAdd);
                            nodes.Add(newTargetNodeToAdd);
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Parse rules from file and apply them on the node list
        /// </summary>
        /// <param name="nodes">List of nodes to modify</param>
        private void ParseRules(List<Node> nodes)
        {
            var ruleLines = InputService.GetRows(MapdataFilePath).FindAll(x => x.Contains(RuleIdentifier));

            foreach (var line in ruleLines)
            {
                foreach (var edgeWord in PossibleEdges)
                {
                    var ruleEdgeWord = RuleEdgeIdentifier + Space + edgeWord;
                    var lineData = line.Replace(RuleIdentifier + Space, Empty);
                    var position = lineData.IndexOf(RuleEdgeIdentifier + Space + edgeWord, StringComparison.Ordinal);

                    if (position != -1)
                    {
                        var newSourceNodeName = lineData.Substring(0, position - SpacePosition);
                        var newTargetNodeName = lineData.Substring(position + SpacePosition + ruleEdgeWord.Length);

                        var newEdgeToAdd = new Edge(edgeWord);
                        var newTargetNodeToAdd = new Node(newTargetNodeName);

                        foreach (var sourceNode in nodes)
                        {
                            if (sourceNode.GetContent().Equals(newSourceNodeName))
                            {
                                sourceNode.AddEdge(newEdgeToAdd);

                                foreach (var targetNode in nodes)
                                {
                                    if (targetNode.GetContent().Equals(newTargetNodeName))
                                    {
                                        newEdgeToAdd.SetNode(targetNode);
                                        newTargetNodeToAdd = targetNode;
                                    }
                                }

                                if (!nodes.Contains(newTargetNodeToAdd))
                                {
                                    newEdgeToAdd.SetNode(newTargetNodeToAdd);
                                    nodes.Add(newTargetNodeToAdd);
                                }

                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}