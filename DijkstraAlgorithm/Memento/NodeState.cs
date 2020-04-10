using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm.Memento
{
    public class NodeState : MementoState
    {
        int id;
        int costValue;

        public object getSave()
        {
            Node node = new Node();
            node.id = id;
            node.costValue = costValue;
            return node;
        }

        public void setSave(object obj)
        {
            Node n = (Node)obj;
            if (n != null)
            {
                id = n.id;
                costValue = n.costValue;
            }
        }
    }
}
