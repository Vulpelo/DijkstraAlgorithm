using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.TextFormatting;

namespace DijkstraAlgorithm
{
    public class NodeElement
    {
        public Node node { get; private set; }
        Button model = null;

        List<EdgeElement> edges = new List<EdgeElement>();
        Node nodeFrom = null;
        Node nodeTo = null;

        public NodeElement(Node _node)
        {
            node = _node;

            model = new Button();
            model.Click += nodeWasClicked;
            model.Width = 40;
            model.Height = 40;

            model.SetValue(Canvas.LeftProperty, node.position.X - model.Width/2);
            model.SetValue(Canvas.TopProperty, node.position.Y - model.Height/2);
            model.Content = node.id.ToString();

            model.AllowDrop = true;
        }

        public Button getModel()
        {
            return model;
        }

        public void addToCanvas(Canvas canvas)
        {
            Canvas.SetZIndex(model, 0);
            canvas.Children.Add(model);
        }

        public void removeFromCanvas(Canvas canvas)
        {
            Master.window.MainCanvas.Children.Remove(getModel());
        }

        public void nodeWasClicked(object sender, RoutedEventArgs e)
        {
            if (Master.actualMode == Mode.ADD_EDGE)
            {
                Master.creatingEdge(this);

            } else if (Master.actualMode == Mode.REMOVE)
            {
                destroy();
            }
        }

        public void removeEdge(EdgeElement edgeElement)
        {
            edges.Remove(edgeElement);
        }

        public void addEdge(NodeElement toNode)
        {
            if (!hasEdge(this, toNode))
            {
                EdgeElement edge = new EdgeElement(this, toNode);
                edge.createLineSegment(Master.window.MainCanvas);
                edges.Add(edge);

                toNode.addEdge(edge);
                addEdge(edge);
            }
        }

        private bool hasEdge(NodeElement from, NodeElement to)
        {
            // TODO: has such edge this<->toNode
            return false;
        }

        public void addEdge(EdgeElement edgeElement)
        {
            this.node.targets.Add( 
                edgeElement.edge.fromNode == this.node ?
                edgeElement.edge.toNode : edgeElement.edge.fromNode);

            edges.Add(edgeElement);
        }

        public void destroy()
        {
            while (edges.Count > 0)
            {
                edges[0].destroy();
            }
            removeFromCanvas(Master.window.MainCanvas);
            Master.removeNode(this.node);
            Master.window.removeNodeElement(this);
        }
    }
}
