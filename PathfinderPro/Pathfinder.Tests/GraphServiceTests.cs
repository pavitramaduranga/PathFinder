using Microsoft.VisualStudio.TestTools.UnitTesting;
using PathfinderPro.Bussiness;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder.Tests
{
    [TestClass]
    public class GraphServiceTests
    {
        private GraphService _graphService;
        private List<Node> _graph;

        [TestInitialize]
        public void Setup()
        {
            _graphService = new GraphService();
            _graph = _graphService.BuildGraph();
        }

        [TestMethod]
        public void TestGraphContainsAllNodes()
        {
            var expectedNodes = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
            CollectionAssert.AreEquivalent(expectedNodes, _graph.Select(node => node.Name).ToList());
        }

        [TestMethod]
        public void TestNodeConnections()
        {
            var nodeA = _graph.FirstOrDefault(n => n.Name == "A");
            Assert.IsNotNull(nodeA);
            Assert.IsTrue(nodeA.Edges.Any(e => e.Target.Name == "B"));
            Assert.IsTrue(nodeA.Edges.Any(e => e.Target.Name == "C"));

            var nodeB = _graph.FirstOrDefault(n => n.Name == "B");
            Assert.IsNotNull(nodeB);
            Assert.IsTrue(nodeB.Edges.Any(e => e.Target.Name == "A"));
            Assert.IsTrue(nodeB.Edges.Any(e => e.Target.Name == "F"));

            var nodeC = _graph.FirstOrDefault(n => n.Name == "C");
            Assert.IsNotNull(nodeC);
            Assert.IsTrue(nodeC.Edges.Any(e => e.Target.Name == "A"));
            Assert.IsTrue(nodeC.Edges.Any(e => e.Target.Name == "D"));

            var nodeD = _graph.FirstOrDefault(n => n.Name == "D");
            Assert.IsNotNull(nodeD);
            Assert.IsTrue(nodeD.Edges.Any(e => e.Target.Name == "C"));
            Assert.IsTrue(nodeD.Edges.Any(e => e.Target.Name == "G"));
            Assert.IsTrue(nodeD.Edges.Any(e => e.Target.Name == "E"));

            var nodeE = _graph.FirstOrDefault(n => n.Name == "E");
            Assert.IsNotNull(nodeE);
            Assert.IsTrue(nodeE.Edges.Any(e => e.Target.Name == "D"));
            Assert.IsTrue(nodeE.Edges.Any(e => e.Target.Name == "F"));
            Assert.IsTrue(nodeE.Edges.Any(e => e.Target.Name == "B"));

            var nodeF = _graph.FirstOrDefault(n => n.Name == "F");
            Assert.IsNotNull(nodeF);
            Assert.IsTrue(nodeF.Edges.Any(e => e.Target.Name == "B"));
            Assert.IsTrue(nodeF.Edges.Any(e => e.Target.Name == "E"));
            Assert.IsTrue(nodeF.Edges.Any(e => e.Target.Name == "H"));

            var nodeG = _graph.FirstOrDefault(n => n.Name == "G");
            Assert.IsNotNull(nodeG);
            Assert.IsTrue(nodeG.Edges.Any(e => e.Target.Name == "D"));
            Assert.IsTrue(nodeG.Edges.Any(e => e.Target.Name == "H"));
            Assert.IsTrue(nodeG.Edges.Any(e => e.Target.Name == "I"));

            var nodeH = _graph.FirstOrDefault(n => n.Name == "H");
            Assert.IsNotNull(nodeH);
            Assert.IsTrue(nodeH.Edges.Any(e => e.Target.Name == "F"));
            Assert.IsTrue(nodeH.Edges.Any(e => e.Target.Name == "G"));
            Assert.IsTrue(nodeH.Edges.Any(e => e.Target.Name == "I"));

            var nodeI = _graph.FirstOrDefault(n => n.Name == "I");
            Assert.IsNotNull(nodeI);
            Assert.IsTrue(nodeI.Edges.Any(e => e.Target.Name == "G"));
            Assert.IsTrue(nodeI.Edges.Any(e => e.Target.Name == "H"));
        }


        [TestMethod]
        public void TestEdgeDistances()
        {
            var nodeA = _graph.FirstOrDefault(n => n.Name == "A");
            Assert.IsNotNull(nodeA);
            Assert.AreEqual(4, nodeA.Edges.FirstOrDefault(e => e.Target.Name == "B").Distance);
            Assert.AreEqual(6, nodeA.Edges.FirstOrDefault(e => e.Target.Name == "C").Distance);

            var nodeB = _graph.FirstOrDefault(n => n.Name == "B");
            Assert.IsNotNull(nodeB);
            Assert.AreEqual(2, nodeB.Edges.FirstOrDefault(e => e.Target.Name == "F").Distance);

            var nodeC = _graph.FirstOrDefault(n => n.Name == "C");
            Assert.IsNotNull(nodeC);
            Assert.AreEqual(8, nodeC.Edges.FirstOrDefault(e => e.Target.Name == "D").Distance);

            var nodeD = _graph.FirstOrDefault(n => n.Name == "D");
            Assert.IsNotNull(nodeD);
            Assert.AreEqual(1, nodeD.Edges.FirstOrDefault(e => e.Target.Name == "G").Distance);
            Assert.AreEqual(4, nodeD.Edges.FirstOrDefault(e => e.Target.Name == "E").Distance);

            var nodeE = _graph.FirstOrDefault(n => n.Name == "E");
            Assert.IsNotNull(nodeE);
            Assert.AreEqual(3, nodeE.Edges.FirstOrDefault(e => e.Target.Name == "F").Distance);
            Assert.AreEqual(2, nodeE.Edges.FirstOrDefault(e => e.Target.Name == "B").Distance);

            var nodeF = _graph.FirstOrDefault(n => n.Name == "F");
            Assert.IsNotNull(nodeF);
            Assert.AreEqual(6, nodeF.Edges.FirstOrDefault(e => e.Target.Name == "H").Distance);

            var nodeG = _graph.FirstOrDefault(n => n.Name == "G");
            Assert.IsNotNull(nodeG);
            Assert.AreEqual(5, nodeG.Edges.FirstOrDefault(e => e.Target.Name == "H").Distance);
            Assert.AreEqual(5, nodeG.Edges.FirstOrDefault(e => e.Target.Name == "I").Distance);

            var nodeH = _graph.FirstOrDefault(n => n.Name == "H");
            Assert.IsNotNull(nodeH);
            Assert.AreEqual(8, nodeH.Edges.FirstOrDefault(e => e.Target.Name == "I").Distance);
        }


        [TestMethod]
        public void TestBidirectionalPaths()
        {
            var bidirectionalEdges = new List<(string, string)>
            {
                ("A", "B"), 
                ("A", "C"),
                ("B", "F"),
                ("C", "D"),
                ("D", "G"), 
                ("D", "E"),
                ("E", "F"),
                ("F", "H"),
                ("G", "H"),
                ("G", "I"),
                ("H", "I")
            };

            foreach (var edge in bidirectionalEdges)
            {
                var node1 = _graph.FirstOrDefault(n => n.Name == edge.Item1);
                var node2 = _graph.FirstOrDefault(n => n.Name == edge.Item2);

                Assert.IsNotNull(node1, $"Node {edge.Item1} not found.");
                Assert.IsNotNull(node2, $"Node {edge.Item2} not found.");

                Assert.IsTrue(node1.Edges.Any(e => e.Target == node2), $"Edge should exist from {edge.Item1} to {edge.Item2}.");
                Assert.IsTrue(node2.Edges.Any(e => e.Target == node1), $"Edge should exist from {edge.Item2} to {edge.Item1}.");
            }
        }


        [TestMethod]
        public void TestUnidirectionalPathFromEToB()
        {
            var nodeE = _graph.FirstOrDefault(n => n.Name == "E");
            Assert.IsNotNull(nodeE, "Node E should exist in the graph.");

            var nodeB = _graph.FirstOrDefault(n => n.Name == "B");
            Assert.IsNotNull(nodeB, "Node B should exist in the graph.");

            // Check that there is an edge from E to B
            var edgeFromEToB = nodeE.Edges.FirstOrDefault(e => e.Target == nodeB);
            Assert.IsNotNull(edgeFromEToB, "There should be an edge from Node E to Node B.");
            Assert.AreEqual(2, edgeFromEToB.Distance, "The distance from Node E to Node B should be 2.");

            // Check that there is no edge from B to E, confirming the unidirectional nature
            var edgeFromBToE = nodeB.Edges.FirstOrDefault(e => e.Target == nodeE);
            Assert.IsNull(edgeFromBToE, "There should not be an edge from Node B to Node E, confirming the unidirectional nature of the path.");
        }

    }
}
