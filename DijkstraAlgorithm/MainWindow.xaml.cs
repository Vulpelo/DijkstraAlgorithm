using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Shapes;


namespace DijkstraAlgorithm
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<NodeElement> nodeElements = new List<NodeElement>();

        System.Windows.Media.Brush selectedColor = System.Windows.Media.Brushes.Gray;
        System.Windows.Media.Brush unselectedColor = System.Windows.Media.Brushes.LightGray;

        public MainWindow()
        {
            InitializeComponent();
            Master.setMainCanvas(MainCanvas);
            Master.window = this;
        }

        public DependencyObject getTemplate(String name)
        {
            return GetTemplateChild(name);
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Master.actualMode == Mode.ADD_NODE)
            {
                Node node = new Node(e.GetPosition(MainCanvas));
                Master.addNode(node);

                NodeElement nodeElement = new NodeElement(node);
                nodeElement.addToCanvas(MainCanvas);
                nodeElements.Add(nodeElement);
            }
        }

        private void addNode_button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            switchEditMode(Mode.ADD_NODE, b);
        }

        private void addEdge_buttonClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            switchEditMode(Mode.ADD_EDGE, b);
        }

        private void remove_ButtonClicked(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            switchEditMode(Mode.REMOVE, b);
        }


        public void removeNodeElement(NodeElement nodeElement)
        {
            nodeElements.Remove(nodeElement);
        }

        public void resetStartNode()
        {
            foreach (NodeElement n in nodeElements)
            {
                if (n.getNodeType() == NodeType.START)
                {
                    n.setNodeType(NodeType.NORMAL);
                    break;
                }
            }
        }

        public void resetEndNode()
        {
            foreach (NodeElement n in nodeElements)
            {
                if (n.getNodeType() == NodeType.END)
                {
                    n.setNodeType(NodeType.NORMAL);
                    break;
                }
            }
        }

        private void switchEditMode(Mode newMode, Button button)
        {
            if (Master.actualMode == newMode)
            {
                Master.actualMode = Mode.NONE;
                button.Background = unselectedColor;
            }
            else
            {
                resetEditButtons();
                Master.actualMode = newMode;
                button.Background = selectedColor;
            }
        }

        private void resetEditButtons()
        {
            addNodeButton.Background = unselectedColor;
            addEdgeButton.Background = unselectedColor;
            removeButton.Background = unselectedColor;
            setStartNodeButton.Background = unselectedColor;
            setEndNodeButton.Background = unselectedColor;
        }

        private void calculateButtonClick(object sender, RoutedEventArgs e)
        {
            DijkstraCalculations dc = new DijkstraCalculations(getDataNodes());
            NodeElement start = getFirstNodeByType(NodeType.START);
            NodeElement end = getFirstNodeByType(NodeType.END);
            dc.calculate(start.node, end.node);
        }

        private NodeElement getFirstNodeByType(NodeType type)
        {
            foreach (NodeElement ne in nodeElements)
            {
                if (ne.getNodeType() == type)
                {
                    return ne;
                }
            }
            return null;
        }

        private List<Node> getDataNodes()
        {
            List<Node> dataNodes = new List<Node>();
            foreach (NodeElement nodeElement in nodeElements)
            {
                dataNodes.Add(nodeElement.node);
            }
            return dataNodes;
        }

        private void setStartNodeButtonClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            switchEditMode(Mode.START_NODE, b);
        }

        private void setEndNodeButtonClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            switchEditMode(Mode.END_NODE, b);
        }
    }
}
