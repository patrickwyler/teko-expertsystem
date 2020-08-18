using System;
using System.Collections.Generic;
using expertsystem.models;

namespace expertsystem.utils
{
    /// <summary>
    /// Utils class for console stuff
    /// </summary>
    public static class ConsoleUtils
    {
        public const string NewLine = "\r\n";

        /// <summary>
        /// Ask user if we should stop the console process
        /// </summary>
        /// <returns>true if user wants to stop the current process</returns>
        public static bool ContinueProcess()
        {
            Console.Write(NewLine + "Continue? [y/n] ");

            ConsoleKey response;
            do
            {
                response = Console.ReadKey(false).Key; // false means display the pressed key

                if (response != ConsoleKey.Enter)
                {
                    Console.WriteLine();
                }
            } while (IsPressedKeyYorN(response));

            // check if pressed key was "Y"
            return response == ConsoleKey.Y;
        }

        /// <summary>
        /// Display selectable node options
        /// </summary>
        /// <param name="nodes">List of nodes</param>
        public static void DisplaySelectableNodeOptions(List<Node> nodes)
        {
            Console.WriteLine(NewLine + NewLine + "Possible nodes to select:");

            for (var i = 0; i < nodes.Count; i++)
            {
                Console.WriteLine("[" + i + "] " + nodes[i].GetContent());
            }
        }

        /// <summary>
        /// Ask user for a specific node from which we should display the connections
        /// </summary>
        /// <param name="nodes">List of nodes</param>
        /// <returns>node or null if we couldn't find the node</returns>
        public static Node AskForSearchNode(List<Node> nodes)
        {
            Console.WriteLine(NewLine + NewLine + "Search for node number:");

            try
            {
                // get number or throw FormatException
                var searchForNodeNumber = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(NewLine + "Result:" + NewLine);

                // return selected node
                return nodes[searchForNodeNumber];
            }
            catch (FormatException)
            {
                Console.WriteLine("Not a number!");
            }
            catch (Exception)
            {
                Console.WriteLine("A Node with this number doesn't exist!");
            }

            return null; // node not found
        }

        /// <summary>
        /// Check if pressed key is Y or N
        /// </summary>
        /// <param name="response">pressed key</param>
        /// <returns>true if one of the two right key was pressed</returns>
        private static bool IsPressedKeyYorN(ConsoleKey response)
        {
            return response != ConsoleKey.Y && response != ConsoleKey.N;
        }
    }
}