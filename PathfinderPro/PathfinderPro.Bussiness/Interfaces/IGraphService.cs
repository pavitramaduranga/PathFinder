﻿using System.Collections.Generic;

namespace PathfinderPro.Bussiness.Interfaces
{
    public interface IGraphService
    {
        List<Node> BuildGraph(string dataFilePath);
        List<string> GetNodesInGraph();
    }
}
