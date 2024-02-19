using System.Collections.Generic;

namespace PathfinderPro.Business.Interfaces
{
    public interface IPathfinderService
    {
        ShortestPathData ShortestPath(string fromNodeName, string toNodeName, List<Node> graphNodes);
    }
}
