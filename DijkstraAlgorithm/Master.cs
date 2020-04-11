using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DijkstraAlgorithm
{
    enum Mode
    {
        NONE,
        ADD_NODE,
        ADD_EDGE,
        REMOVE
    }

    static class Master
    {
        static public MainWindow window { get; set; } = null;

        static Canvas mainCanvas = null;

        static NodeElement nodeFrom = null;
        static NodeElement nodeTo = null;

        static List<Node> nodes = new List<Node>();

        //static List<EdgeElement> edges = new List<EdgeElement>();


        static public DependencyObject getTemplate(String name)
        {
            return window.getTemplate(name);
        }
        static private Mode _actualMode = Mode.NONE;
        static public Mode actualMode { get { return _actualMode; } set { _actualMode = value; resetOnModeChange(); } }

        static public void creatingEdge(NodeElement node)
        {
            if (nodeFrom == null)
            {
                nodeFrom = node;
                return;
            } else if (nodeFrom != node)
            {
                nodeTo = node;
                nodeFrom.addEdge(nodeTo);

                nodeFrom = null;
                nodeTo = null;
            }
        }

        static private void resetOnModeChange()
        {
            nodeFrom = null;
            nodeTo = null;
        }

        static public void setMainCanvas(Canvas mainCanvas)
        {
            Master.mainCanvas = mainCanvas;
        }

        static public void removeNode(Node node)
        {
            foreach (Node n in node.targets)
            {
                n.targets.Remove(node);
            }
            nodes.Remove(node);
        }

        static public void addNode(Node node)
        {
            nodes.Add(node);
        }

        static public void removeEdge(Node from, Node to)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] == from)
                {
                    for (int j = 0; j < nodes[i].targets.Count; j++)
                    {
                        if (nodes[i].targets[j] == to)
                        {
                            nodes[i].targets[j].targets.Remove(from);
                            nodes[i].targets.Remove(to);
                            break;
                        }
                    }
                    break;
                }
            }
        }

    }
}
