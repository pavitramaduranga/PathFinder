using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathfinderPro.Business;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pathfinder.Tests
{
    [TestClass]
    public class PathfinderServiceTests
    {
        private PathfinderService _pathfinderService;
        private GraphService _graphService;
        private List<Node> _graph;
        private readonly string dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "GraphData.json");
        
        [TestInitialize]
        public void Setup()
        {
            _pathfinderService = new PathfinderService();
            _graphService = new GraphService();
            _graph = _graphService.GetGraph(dataFilePath);
        }

        [TestMethod]
        public void TestShortestPath()
        {
            string startNodeName = "A";
            string endNodeName = "E";

            int expectedDistance = 9;
            List<string> expectedPath = new List<string> { "A", "B", "F", "E" };

            var result = _pathfinderService.ShortestPath(startNodeName, endNodeName, _graph);

            Assert.IsNotNull(result, "Result should not be null for a valid path.");
            Assert.AreEqual(expectedDistance, result.Distance, "The calculated distance is incorrect.");
            CollectionAssert.AreEqual(expectedPath, result.NodeNames, "The calculated path is incorrect.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestShortestPathWithInvalidStartNode()
        {
            string startNodeName = "K";
            string endNodeName = "E";
            _pathfinderService.ShortestPath(startNodeName, endNodeName, _graph);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestShortestPathWithInvalidEndNode()
        {
            string startNodeName = "A";
            string endNodeName = "J";
            _pathfinderService.ShortestPath(startNodeName, endNodeName, _graph);
        }

        [TestMethod]
        public void TestShortestPathWithUnreachableDestination()
        {
            // Setup a graph where a node is unreachable
            // A --1--> B, with C being unreachable
            Node nodeA = new Node { Name = "A", Edges = new List<Edge>() };
            Node nodeB = new Node { Name = "B", Edges = new List<Edge>() };
            Node nodeC = new Node { Name = "C", Edges = new List<Edge>() }; // Unreachable node

            nodeA.Edges.Add(new Edge { Target = nodeB, Distance = 1 });

            List<Node> graph = new List<Node> { nodeA, nodeB, nodeC };

            PathfinderService pathfinderService = new PathfinderService();

            string startNodeName = "A";
            string endNodeName = "C";

            var result = pathfinderService.ShortestPath(startNodeName, endNodeName, graph);

            Assert.IsNull(result, "Result should be null for an unreachable destination.");
        }

    }
}
