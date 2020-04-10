using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm
{
    public class Edge
    {
        public Node fromNode { get; private set; } = null;
        public Node toNode { get; private set; } = null;
        public int cost { get; set; } = 0;

        public Edge(Node from, Node to)
        {
            fromNode = from;
            toNode = to;
        }
    }
}
