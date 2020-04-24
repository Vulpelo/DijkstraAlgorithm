using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm
{
    class GraphGenerator
    {
        int amountOfNodes;
        int amountOfEdges;
        int costFrom;
        int costTo;

        Random rand = new Random();

        List<Node> nodes = null;

        public List<Node> generate(int amountOfNodes, int amountOfEdges, int costFrom, int costTo)
        {
            if (amountOfNodes > 0 &&
                amountOfEdges >= amountOfNodes - 1 &&
                maxEdgesForNodes(amountOfNodes) >= amountOfEdges &&
                costFrom >= 0 && costTo >= costFrom)
            {
                this.amountOfNodes = amountOfNodes;
                this.amountOfEdges = amountOfEdges;
                this.costFrom = costFrom;
                this.costTo = costTo;

                // create nodes
                nodes = new List<Node>();
                for (int i = 0; i < amountOfNodes; i++)
                {
                    nodes.Add( new Node() );
                }

                // at first go thru nodes, to ensure each node has at least one edge.
                // get fist node, select next random, create edge. Go to next random, create edge between second and third
                for (int i = 0; i < amountOfNodes; i++)
                {
                }
                
                // repeat amountOfEdge times: get random two different nodes. 
                //      connect them with edge
                //      random cost, set value to edge
                for (int i = 0; i < amountOfEdges; i++)
                {
                    int nodeIndex1 = -1;
                    int nodeIndex2 = -1;
                    while (nodeIndex2 == -1)
                    {
                        nodeIndex1 = randNode();
                        nodeIndex2 = randNode(nodeIndex1);
                    }

                    createEdge(nodes[nodeIndex1], nodes[nodeIndex2]);
                }

                return nodes;
            }
            return null;
        }

        private void createEdge(Node fromNode, Node toNode)
        {
            int edgeCost = randCost();
            fromNode.targets.Add(toNode, edgeCost);
            toNode.targets.Add(fromNode, edgeCost);
        }

        private int maxEdgesForNodes(int nodes)
        {
            int s = 0;
            for (int i = 1; i <= nodes; i++)
            {
                s += i;
            }
            return s;
        }

        private int randCost()
        {
            return rand.Next(costFrom, costTo);
        }

        // get random node that isnt already connected with edge to 'ignore' one
        private int randNode(int ignore = -1)
        {
            int indexNode = rand.Next(amountOfNodes);
            if (ignore == indexNode)
            {
                indexNode = (indexNode >= nodes.Count - 1) ? 0 : indexNode++;
            }

            if (ignore >= 0)
            {
                int i = 0;
                for (i = 0; 
                    i < nodes.Count && areEdgeConnected(nodes.ElementAt(indexNode), nodes.ElementAt(ignore)); 
                    i++)
                {
                    indexNode = (indexNode >= nodes.Count - 1) ? 0 : indexNode++;

                }

                if (i == nodes.Count)
                {
                    return -1;
                }
            }

            return indexNode;
        }

        private bool areEdgeConnected(Node node1, Node node2)
        {
            return node1 == node2 || node1.targets.ContainsKey(node2);
        }
    }
}
