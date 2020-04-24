using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm
{
    public enum NodeSearchState
    {
        USED,
        ACTUAL,
        COMPARING,
        NOT_USED
    };

    public enum NodeType
    {
        START,
        END,
        NORMAL
    };

    public class Node
    {
        static int _id = 0;
        public int id { get; set; } = 0;
        public string name { get; set; } = "";
        public NodeSearchState searchState { get; set; } = NodeSearchState.NOT_USED;
        public NodeType nodeType { get; set; } = NodeType.NORMAL;

        static public void resetIDs()
        {
            _id = 0;
        }

        public int costValue { get; set; } = int.MaxValue;
        public System.Windows.Point position { get; set; }

        public int costValueCompareTo { get; set; } = -1;

        //public List<Node> targets { get; set; } = new List<Node>();
        public Dictionary<Node, int> targets { get; set; } = new Dictionary<Node, int>();

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
