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

                Node bestNode = null;
                
                foreach (Node n in actual.targets.Keys)
                {
                    if (n.searchState == NodeSearchState.NOT_USED)
                    {
                        // calculate
                        int newCost = actual.targets[n] + (actual.costValue == int.MaxValue ? 0 : actual.costValue);

                        n.searchState = NodeSearchState.COMPARING;
                        n.costValueCompareTo = newCost;
                        nodeCollection.savePhase(nodes);
                        n.searchState = NodeSearchState.NOT_USED;

                        if (n.costValue > newCost)
                        {
                            n.costValue = newCost;
                        }

                        nodeCollection.savePhase(nodes);

                        // select
                        if (bestNode == null || n.costValue < bestNode.costValue)
                        {
                            bestNode = n;
                        }
                    }
                }
                actual.searchState = NodeSearchState.USED;
                if (bestNode == null) return;
                actual = bestNode;
            }
            actual.searchState = NodeSearchState.ACTUAL;
            nodeCollection.savePhase(nodes);

        }

        private void reset()
        {
            foreach (Node n in nodes)
            {
                n.searchState = NodeSearchState.NOT_USED;
                n.costValue = int.MaxValue;
            }
        }

        private void savePhaseState()
        {
            nodeCollection.savePhase(nodes);
        }

        public void loadPhaseState(int index)
        {
            List<Memento.Memento> newNodes_states = nodeCollection.getMementosPhase(index);
            if (newNodes_states != null)
            {
                for (int i=0; i < newNodes_states.Count; i++)
                {
                    Node newNode = (Node)newNodes_states[i].getState().getSave();
                    if (newNode != null)
                    {
                        nodes[i].id = newNode.id;
                        nodes[i].searchState = newNode.searchState;
                        nodes[i].costValue = newNode.costValue;
                        nodes[i].costValueCompareTo = newNode.costValueCompareTo;
                    }
                }
            }
        }

        public int amountOfCalculationSteps()
        {
            return nodeCollection.amountOfMementosPhases();
        }
    }
}
