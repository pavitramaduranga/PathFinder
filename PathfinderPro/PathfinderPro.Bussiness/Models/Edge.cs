using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderPro.Bussiness
{
    public class Edge
    {
        public Node Target { get; set; }
        public int Distance { get; set; }
    }
}
