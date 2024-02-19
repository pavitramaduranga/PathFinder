using Newtonsoft.Json;
using PathfinderPro.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PathfinderPro.Business
{
    public class GraphService : IGraphService
    {
        private List<Node> _graph;
        private readonly object _lock = new object();
        private bool _isGraphBuilt = false;

        public GraphService()
        {
            _graph = new List<Node>();
        }

        public List<string> GetNodesInGraph() {
            return _graph.Select(node => node.Name).ToList();
        }

        public List<Node> GetGraph(string dataFilePath)
        {
            if (string.IsNullOrEmpty(dataFilePath))
            {
                throw new ArgumentException("Data file path not found.");
            }

            if (!_isGraphBuilt)
            {
                lock (_lock)
                {
                    if (!_isGraphBuilt)
                    {
                        _graph = BuildGraphFromJson(dataFilePath);
                        _isGraphBuilt = true;
                    }
                }
            }
            return _graph;
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
