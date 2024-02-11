﻿using PathfinderPro.Bussiness;
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
            IGraphService graphService = new GraphService();
            string dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "GraphData.json");
            List<Node> graphNodes = graphService.BuildGraph(dataFilePath);

            Console.WriteLine("Enter the FROM node:");
            string fromNode = Console.ReadLine();

            Console.WriteLine("Enter the TO node:");
            string toNode = Console.ReadLine();

            IPathfinderService service = new PathfinderService();
            ShortestPathData result = service.ShortestPath(fromNode, toNode, graphNodes);

            Console.WriteLine("Shortest Path: " + string.Join(", ", result.NodeNames));
            Console.WriteLine("Total Distance: " + result.Distance);
            Console.ReadLine();
        }
    }
}
