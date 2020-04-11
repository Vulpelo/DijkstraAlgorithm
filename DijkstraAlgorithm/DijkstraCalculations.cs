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

        }
        public DijkstraCalculations(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public void calculate()
        {

        }

        public void nextStep()
        {

        }

        public void prevStep()
        {

        }

        private void saveState()
        {
            foreach (Node node in nodes)
            {
                nodeCollection.saveState(node);
            }
        }
    }
}
