using PathfinderPro.Bussiness.Interfaces;
using System.Collections.Generic;

namespace PathfinderPro.Bussiness
{
    public class GraphService : IGraphService
    {
        public List<Node> BuildGraph()
        {
            Node A = new Node { Name = "A" };
            Node B = new Node { Name = "B" };
            Node C = new Node { Name = "C" };
            Node D = new Node { Name = "D" };
            Node E = new Node { Name = "E" };
            Node F = new Node { Name = "F" };
            Node G = new Node { Name = "G" };
            Node H = new Node { Name = "H" };
            Node I = new Node { Name = "I" };

            // Add edges for A
            A.Edges.Add(new Edge { Target = B, Distance = 4 });
            A.Edges.Add(new Edge { Target = C, Distance = 6 });
            // Add reverse edge for bidirectional path
            B.Edges.Add(new Edge { Target = A, Distance = 4 });
            C.Edges.Add(new Edge { Target = A, Distance = 6 });

            // Add edges for B
            B.Edges.Add(new Edge { Target = F, Distance = 2 });
            // Add reverse edge for bidirectional path
            F.Edges.Add(new Edge { Target = B, Distance = 2 });

            // Add edges for C
            C.Edges.Add(new Edge { Target = D, Distance = 8 });
            // Add reverse edge for bidirectional path
            D.Edges.Add(new Edge { Target = C, Distance = 8 });

            // Add edges for D
            D.Edges.Add(new Edge { Target = G, Distance = 1 });
            // Add reverse edge for bidirectional path
            G.Edges.Add(new Edge { Target = D, Distance = 1 });

            // Add edges for E
            E.Edges.Add(new Edge { Target = D, Distance = 4 });
            // Add reverse edge for bidirectional path
            D.Edges.Add(new Edge { Target = E, Distance = 4 });

            E.Edges.Add(new Edge { Target = F, Distance = 3 });
            // Add reverse edge for bidirectional path
            F.Edges.Add(new Edge { Target = E, Distance = 3 });

            // Unidirectional edge from E to B
            E.Edges.Add(new Edge { Target = B, Distance = 2 });

            // Add edges for F
            F.Edges.Add(new Edge { Target = H, Distance = 6 });
            // Add reverse edge for bidirectional path
            H.Edges.Add(new Edge { Target = F, Distance = 6 });

            // Add edges for G
            G.Edges.Add(new Edge { Target = H, Distance = 5 });
            // Add reverse edge for bidirectional path
            H.Edges.Add(new Edge { Target = G, Distance = 5 });

            G.Edges.Add(new Edge { Target = I, Distance = 5 });
            // Add reverse edge for bidirectional path
            I.Edges.Add(new Edge { Target = G, Distance = 5 });

            // Add edges for H
            H.Edges.Add(new Edge { Target = I, Distance = 8 });
            // Add reverse edge for bidirectional path
            I.Edges.Add(new Edge { Target = H, Distance = 8 });

            // The graph is represented as a list of nodes
            List<Node> graph = new List<Node> { A, B, C, D, E, F, G, H, I };

            return graph;
        }
    }
}
