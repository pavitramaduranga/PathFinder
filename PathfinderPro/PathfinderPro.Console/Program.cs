using PathfinderPro.Bussiness;
using PathfinderPro.Bussiness.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace PathfinderPro.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            BuildGraph(out IGraphService graphService, out List<Node> graphNodes);

            var nodesInGraph = graphService.GetNodesInGraph();
            DisplayAvailableNodes(nodesInGraph);

            Console.WriteLine("Type 'exit' to quit");

            while (true)
            {
                Console.WriteLine("Enter the FROM node :");
                string fromNode = Console.ReadLine();

                if (fromNode.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                Console.WriteLine("Enter the TO node :");
                string toNode = Console.ReadLine();

                if (toNode.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                bool fromNodeExists = nodesInGraph.Contains(fromNode);
                bool toNodeExists = nodesInGraph.Contains(toNode);

                if (!fromNodeExists || !toNodeExists)
                {
                    DispalyErrorMessages(fromNode, toNode, fromNodeExists, toNodeExists);
                }
                else
                {
                    FindPath(graphNodes, fromNode, toNode);
                }

                Console.WriteLine("\n--- Press any key to continue or type 'exit' to quit ---");
                if (Console.ReadLine().Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }
        }

        private static void FindPath(List<Node> graphNodes, string fromNode, string toNode)
        {
            IPathfinderService service = new PathfinderService();
            ShortestPathData result = service.ShortestPath(fromNode, toNode, graphNodes);
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (result != null)
            {
                Console.WriteLine("Shortest Path: " + string.Join(", ", result.NodeNames));
                Console.WriteLine("Total Distance: " + result.Distance);
            }
            else
            {
                Console.WriteLine("No path found.");
            }
            Console.ResetColor();
        }

        private static void DispalyErrorMessages(string fromNode, string toNode, bool fromNodeExists, bool toNodeExists)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (!fromNodeExists)
            {
                Console.WriteLine($"The node '{fromNode}' does not exist in the graph.");
            }
            if (!toNodeExists)
            {
                Console.WriteLine($"The node '{toNode}' does not exist in the graph.");
            }
            Console.ResetColor();
        }

        private static void BuildGraph(out IGraphService graphService, out List<Node> graphNodes)
        {
            graphService = new GraphService();
            string dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "GraphData.json");
            graphNodes = graphService.GetGraph(dataFilePath);
        }

        private static void DisplayAvailableNodes(List<string> nodesInGraph)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Nodes in the Graph:");
            foreach (string node in nodesInGraph)
            {
                Console.WriteLine($"- {node}");
            }
            Console.ResetColor();
            Console.WriteLine("-------------------------------------------------");
        }
    }
}
