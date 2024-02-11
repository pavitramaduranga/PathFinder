using PathfinderPro.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathfinderPro.Business
{
    public class PathfinderService : IPathfinderService
    {
        public ShortestPathData ShortestPath(string fromNodeName, string toNodeName, List<Node> graphNodes)
        {
            var distances = graphNodes.ToDictionary(node => node.Name, node => int.MaxValue);
            var previous = new Dictionary<string, string>();

            // Using a list to represent nodes to visit
            var nodesToVisit = new List<Node>();

            // Set the start node's distance to 0 and add to the list
            Node startNode = graphNodes.FirstOrDefault(node => node.Name == fromNodeName);
            if (startNode == null)
                throw new ArgumentException("Start node not found in graph");

            distances[startNode.Name] = 0;
            nodesToVisit.Add(startNode);

            while (nodesToVisit.Count > 0)
            {
                // Order by distance and take the first node
                Node currentNode = nodesToVisit.OrderBy(node => distances[node.Name]).First();
                nodesToVisit.Remove(currentNode);

                // If destination is reached
                if (currentNode.Name == toNodeName)
                    break;

                foreach (Edge edge in currentNode.Edges)
                {
                    Node neighbor = edge.Target;
                    int totalDistance = distances[currentNode.Name] + edge.Distance;

                    if (totalDistance < distances[neighbor.Name])
                    {
                        distances[neighbor.Name] = totalDistance;
                        previous[neighbor.Name] = currentNode.Name;

                        // Add the neighbor to the list if it's not already there
                        if (!nodesToVisit.Contains(neighbor))
                        {
                            nodesToVisit.Add(neighbor);
                        }
                    }
                }
            }

            // If the destination is unreachable
            if (distances[toNodeName] == int.MaxValue)
            {
                return null;
            }

            // Reconstruct the shortest path
            var path = new List<string>();
            string current = toNodeName;
            while (!string.IsNullOrEmpty(current))
            {
                path.Add(current);
                if (!previous.TryGetValue(current, out current))
                {
                    break; // We've reached the start node
                }
            }
            path.Reverse();

            // Construct the result
            return new ShortestPathData
            {
                NodeNames = path,
                Distance = distances[toNodeName]
            };
        }
    }
}
