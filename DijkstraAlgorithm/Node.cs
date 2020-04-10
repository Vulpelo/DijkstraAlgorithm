using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm
{
    public class Node
    {
        public int id { get; set; } = 0;
        public int costValue { get; set; } = int.MaxValue;
        public string name { get; set; } = "";
        public System.Windows.Point position { get; set; }
    }
}
