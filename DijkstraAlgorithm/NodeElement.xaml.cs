using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DijkstraAlgorithm
{
    /// <summary>
    /// Interaction logic for NodeElementRenderer.xaml
    /// </summary>
    public partial class NodeElement : UserControl
    {
        public NodeElement()
        {
            InitializeComponent();
        }

        public float getWidthOrigin()
        {
            return 20;
        }
        public float getHeightOrigin()
        {
            return 40;
        }

        System.Windows.Media.Brush startNodeBorderColor = System.Windows.Media.Brushes.LimeGreen;
        System.Windows.Media.Brush endNodeBorderColor = System.Windows.Media.Brushes.Red;
        System.Windows.Media.Brush normalNodeBorderColor = System.Windows.Media.Brushes.Gray;

        System.Windows.Media.Brush usedNodeBgColor = System.Windows.Media.Brushes.IndianRed;
        System.Windows.Media.Brush actualNodeBgColor = System.Windows.Media.Brushes.LightBlue;
        System.Windows.Media.Brush notUsedNodeBgColor = System.Windows.Media.Brushes.Gray;

        System.Windows.Media.Brush comparingNodeBgColor = System.Windows.Media.Brushes.LightGreen;

        System.Windows.Media.Brush comparingNodeLabelColor = System.Windows.Media.Brushes.Gray;
        System.Windows.Media.Brush notComparingNodeLabelColor = System.Windows.Media.Brushes.LightBlue;


        public Node node { get; private set; }

        List<EdgeElement> edges = new List<EdgeElement>();

        public NodeElement(Node _node)
        {
            InitializeComponent();

            node = _node;
            SetValue(Canvas.LeftProperty, node.position.X - getWidthOrigin());
            SetValue(Canvas.TopProperty, node.position.Y - getHeightOrigin());

            this.button.Click += nodeWasClicked;
            this.button.Content = node.id.ToString();

            this.label.Opacity = 0;

            normalNodeBorderColor = this.button.BorderBrush;
            notUsedNodeBgColor = this.button.Background;
        }

        public void showLabel()
        {
            this.label.Opacity = 100;
        }

        public void setInfo(string infoText)
        {
            this.label.Content = infoText;
        }

        public void addToCanvas(Canvas canvas)
        {
            Canvas.SetZIndex(this, 0);
            canvas.Children.Add(this);
        }

        public void removeFromCanvas(Canvas canvas)
        {
            Master.window.MainCanvas.Children.Remove(this);
        }

        public void nodeWasClicked(object sender, RoutedEventArgs e)
        {
            if (Master.actualMode == Mode.ADD_EDGE)
            {
                Master.creatingEdge(this);

            }
            else if (Master.actualMode == Mode.REMOVE)
            {
                destroy();
            }
            else if (Master.actualMode == Mode.START_NODE)
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
            if (!hasEdge(toNode))
            {
                EdgeElement edge = new EdgeElement(this, toNode);
                edge.createLineSegment(Master.window.MainCanvas);
                edges.Add(edge);

                toNode.addEdge(edge);
                addEdge(edge);
            }
        }

        private bool hasEdge(NodeElement toNode)
        {
            foreach (EdgeElement ee in this.edges)
            {
                if (ee.connectedTo(toNode) && ee.connectedTo(this))
                {
                    return true;
                }
            }
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
                    this.button.BorderBrush = startNodeBorderColor;
                    break;
                case NodeType.NORMAL:
                    this.button.BorderBrush = normalNodeBorderColor;
                    break;
                case NodeType.END:
                    this.button.BorderBrush = endNodeBorderColor;
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
                    this.button.Background = actualNodeBgColor;
                    break;
                case NodeSearchState.NOT_USED:
                    this.button.Background = notUsedNodeBgColor;
                    break;
                case NodeSearchState.USED:
                    this.button.Background = usedNodeBgColor;
                    break;
                case NodeSearchState.COMPARING:
                    this.button.Background = comparingNodeBgColor;
                    break;
            }
            if (NodeSearchState.COMPARING == searchState)
            {
                this.label.Background = comparingNodeLabelColor;
            } else
            {
                this.label.Background = notComparingNodeLabelColor;
            }
        }

        public void updateRenderChanges()
        {
            showLabel();
            setNodeType(node.nodeType);
            setSearchState(node.searchState);

            string valText = "";
            if (node.costValue == int.MaxValue)
            {
                valText += "inf";
            }
            else
            {
                valText += node.costValue.ToString();
            }

            if (node.searchState == NodeSearchState.COMPARING)
            {
                setInfo(node.costValueCompareTo.ToString() + " < " + valText);
            } else
            {
                setInfo(valText);
            }
        }
    }
}
