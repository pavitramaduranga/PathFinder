using System.Collections.Generic;

namespace PathfinderPro.Business
{
    public class Node
    {
        public string Name { get; set; }
        public List<Edge> Edges { get; set; } = new List<Edge>();
    }
}
