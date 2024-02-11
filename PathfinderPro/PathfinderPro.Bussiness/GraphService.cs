using Newtonsoft.Json;
using PathfinderPro.Bussiness.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace PathfinderPro.Bussiness
{
    public class GraphService : IGraphService
    {
        public List<Node> BuildGraph(string dataFilePath)
        {
            
            if (string.IsNullOrEmpty(dataFilePath))
            {
                throw new ArgumentException("Data file path not found.");
            }
            List<Node> graph = BuildGraphFromJson(dataFilePath);
            return graph;
        }

        private List<Node> BuildGraphFromJson(string dataFilePath)
        {
            string json = File.ReadAllText(dataFilePath);
            var tempNodes = JsonConvert.DeserializeObject<List<TempNode>>(json);
            var nodeDictionary = new Dictionary<string, Node>();

            // First pass: create all nodes
            foreach (var tempNode in tempNodes)
            {
                var node = new Node { Name = tempNode.Name, Edges = new List<Edge>() };
                nodeDictionary.Add(node.Name, node);
            }

            // Second pass: create edges
            foreach (var tempNode in tempNodes)
            {
                var node = nodeDictionary[tempNode.Name];
                foreach (var tempEdge in tempNode.Edges)
                {
                    var edge = new Edge
                    {
                        Target = nodeDictionary[tempEdge.Target],
                        Distance = tempEdge.Distance
                    };
                    node.Edges.Add(edge);
                }
            }

            return new List<Node>(nodeDictionary.Values);
        }

        private class TempNode
        {
            public string Name { get; set; }
            public List<TempEdge> Edges { get; set; }
        }

        private class TempEdge
        {
            public string Target { get; set; }
            public int Distance { get; set; }
        }
    }
}
