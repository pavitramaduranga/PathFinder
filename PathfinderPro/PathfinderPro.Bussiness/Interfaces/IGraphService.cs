using System.Collections.Generic;

namespace PathfinderPro.Bussiness.Interfaces
{
    public interface IGraphService
    {
        List<Node> GetGraph(string dataFilePath);
        List<string> GetNodesInGraph();
    }
}
