﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderPro.Business.Interfaces
{
    public interface IPathfinderService
    {
        ShortestPathData ShortestPath(string fromNodeName, string toNodeName, List<Node> graphNodes);
    }
}
