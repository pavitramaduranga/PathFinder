﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderPro.Bussiness
{
    public class Node
    {
        public string Name { get; set; }
        public List<Edge> Edges { get; set; } = new List<Edge>();
    }
}
