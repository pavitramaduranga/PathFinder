using System.Collections.Generic;

namespace PathfinderPro.Business.Interfaces
{
    public interface IGraphService
    {
        List<Node> GetGraph(string dataFilePath);
        List<string> GetNodesInGraph();
    }
}
