using System;
using System.Collections.Generic;
using expertsystem.models;
using expertsystem.parser;
using static expertsystem.utils.ConsoleUtils;

namespace expertsystem.services
{
    /// <summary>
    /// Service for searching connections between nodes.
    /// </summary>
    public class SearchService

    {
        private DataParser DataParser { get; } = new DataParser();
        private List<Node> Nodes;

        /// <summary>
        /// Constructor
        ///
        /// Prepare the map out of the data files
        /// </summary>
        public SearchService()
        {
            // Parse 
            Nodes = DataParser.Parse();
        }

        /// <summary>
        /// Display the selectable nodes and let the user search for node connections
        /// </summary>
        public void Search()
        {
            DisplaySelectableNodeOptions(Nodes);

            // let the user search through the data
            do
            {
                // user should select a node
                var node = AskForSearchNode(Nodes);

                // check if node was found
                if (node != null)
                {
                    // print tree to console
                    node.PrintTree("", true);
                }
            } while (ContinueProcess()); // continue searching as long as the user not interrupt the process

            Console.Write(NewLine + "Exit!");
            Console.ReadLine(); // otherwise terminal will be closed automatically
        }
    }
}