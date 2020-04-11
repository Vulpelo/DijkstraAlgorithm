using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm
{
    class DijkstraCalculations
    {
        List<Node> nodes;

        Memento.MementoCollection nodeCollection = new Memento.MementoCollection();

        public DijkstraCalculations()
        {
            nodes = new List<Node>();
        }
        public DijkstraCalculations(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public void calculate(Node startNode, Node endNode)
        {
            reset();
            startNode.costValue = 0;

            Node actual = startNode;
            for (int i = 0; i < nodes.Count && actual != endNode; i++)
            {
                actual.searchState = NodeSearchState.ACTUAL;
                nodeCollection.savePhase(nodes);

                Node bestNode = actual.targets.Count == 0 ? null : actual.targets.Keys.First<Node>();
                
                foreach (Node n in actual.targets.Keys)
                {
                    // calculate
                    int newCost = actual.targets[n] + actual.costValue;
                    if (n.costValue > newCost)
                    {
                        n.costValue = newCost;
                    }

                    // select
                    if (n.costValue < bestNode.costValue)
                    {
                        bestNode = n;
                    }
                }
                actual.searchState = NodeSearchState.USED;
                if (bestNode == null) return;
                actual = bestNode;
            }


        }

        private void reset()
        {
            foreach (Node n in nodes)
            {
                n.searchState = NodeSearchState.NOT_USED;
                n.costValue = int.MaxValue;
            }
        }

        public void nextStep()
        {

        }

        public void prevStep()
        {

        }

        private void savePhaseState()
        {
            nodeCollection.savePhase(nodes);
        }
    }
}
