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
        System.Windows.Media.Brush startNodeBorderColor = System.Windows.Media.Brushes.LimeGreen;
        System.Windows.Media.Brush endNodeBorderColor = System.Windows.Media.Brushes.Red;
        System.Windows.Media.Brush normalNodeBorderColor = System.Windows.Media.Brushes.Gray;

        System.Windows.Media.Brush usedNodeBgColor = System.Windows.Media.Brushes.IndianRed;
        System.Windows.Media.Brush actualNodeBgColor = System.Windows.Media.Brushes.LightBlue;
        System.Windows.Media.Brush notUsedNodeBgColor = System.Windows.Media.Brushes.Gray;

        public Node node { get; private set; }
        Button model = null;

        List<EdgeElement> edges = new List<EdgeElement>();

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
            model.BorderThickness = new Thickness(2);

            normalNodeBorderColor = model.BorderBrush;
            notUsedNodeBgColor = model.Background;

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
            } else if (Master.actualMode == Mode.START_NODE)
            {
                Master.window.resetStartNode();
                setNodeType(NodeType.START);
                //node.nodeType = NodeType.START;
            }
            else if (Master.actualMode == Mode.END_NODE)
            {
                Master.window.resetEndNode();
                setNodeType(NodeType.END);
                //node.nodeType = NodeType.END;
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

        public void setNodeType(NodeType type)
        {
            node.nodeType = type;

            switch (type)
            {
                case NodeType.START:
                    getModel().BorderBrush = startNodeBorderColor;
                    break;
                case NodeType.NORMAL:
                    getModel().BorderBrush = normalNodeBorderColor;
                    break;
                case NodeType.END:
                    getModel().BorderBrush = endNodeBorderColor;
                    break;
            }
        }

        public NodeType getNodeType()
        {
            return node.nodeType;
        }

        public void setSearchState(NodeSearchState searchState)
        {
            node.searchState = searchState;
            switch (searchState)
            {
                case NodeSearchState.ACTUAL:
                    getModel().Background = actualNodeBgColor;
                    break;
                case NodeSearchState.NOT_USED:
                    getModel().Background = notUsedNodeBgColor;
                    break;
                case NodeSearchState.USED:
                    getModel().Background = usedNodeBgColor;
                    break;
            }
        }

        public void updateRenderChanges()
        {
            setNodeType(node.nodeType);
            setSearchState(node.searchState);
        }
    }
}
