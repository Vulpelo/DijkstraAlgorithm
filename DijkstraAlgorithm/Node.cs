using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm
{
    public class Node
    {
        static int _id = 0;
        public int id { get; set; } = 0;
        public int costValue { get; set; } = int.MaxValue;
        public string name { get; set; } = "";
        public System.Windows.Point position { get; set; }

        public List<Node> targets { get; set; } = new List<Node>();

        public Node() {
            id = _id++;
        }
        public Node(int id)
        {
            this.id = id;
        }
        public Node(System.Windows.Point position)
        {
            id = _id++;
            this.position = position;
        }
    }
}
