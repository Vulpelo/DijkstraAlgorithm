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

        static Node nodeFrom = null;
        static Node nodeTo = null;

        static List<EdgeElement> edges = new List<EdgeElement>();
        static Canvas mainCanvas = null;
        static List<NodeElement> nodes = new List<NodeElement>();


        static public DependencyObject getTemplate(String name)
        {
            return window.getTemplate(name);
        }
        static private Mode _actualMode = Mode.NONE;
        static public Mode actualMode { get { return _actualMode; } set { _actualMode = value; resetOnModeChange(); } }

        static public void creatingEdge(Node node)
        {
            if (nodeFrom == null)
            {
                nodeFrom = node;
                return;
            } else if (nodeFrom != node)
            {
                nodeTo = node;
                addEdge(nodeFrom, nodeTo);
                nodeFrom = null;
                nodeTo = null;
            }
        }

        static private void resetOnModeChange()
        {
            nodeFrom = null;
            nodeTo = null;
        }

        static private void addEdge(Node fromNode, Node toNode)
        {
            EdgeElement edge = new EdgeElement(fromNode, toNode);
            edge.createLineSegment(Master.mainCanvas);
            edges.Add(edge);
        }

        static public void setMainCanvas(Canvas mainCanvas)
        {
            Master.mainCanvas = mainCanvas;
        }

        static public void removeNode(NodeElement node)
        {
            mainCanvas.Children.Remove(node.getModel());
            nodes.Remove(node);
        }

        static public void addNode(NodeElement node)
        {
            mainCanvas.Children.Add(node.getModel());
            nodes.Add(node);
        }

        static public List<EdgeElement> getEdgesForNode(Node node)
        {
            List<EdgeElement> edgesFromNode = new List<EdgeElement>();

            foreach (EdgeElement edge in edges)
            {
                if(edge.edge.fromNode == node)
                {
                    edgesFromNode.Add(edge);
                }
            }
            return edgesFromNode;
        }
    }
}
