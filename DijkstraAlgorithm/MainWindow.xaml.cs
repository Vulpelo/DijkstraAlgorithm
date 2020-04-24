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
        DijkstraCalculations dc;
        int currentCalculateIndex = 0;

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
            currentCalculateIndex = 0;
            dc = new DijkstraCalculations(getDataNodes());
            NodeElement start = getFirstNodeByType(NodeType.START);
            NodeElement end = getFirstNodeByType(NodeType.END);
            if (end != null && start != null)
            {
                dc.calculate(start.node, end.node);
                changeCalculationPhase(0);
            }
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

        private void previousButtonClick(object sender, RoutedEventArgs e)
        {
            if (currentCalculateIndex > 0) currentCalculateIndex--;
            changeCalculationPhase(currentCalculateIndex);
        }

        private void nextButtonClick(object sender, RoutedEventArgs e)
        {
            if (++currentCalculateIndex >= dc.amountOfCalculationSteps()) currentCalculateIndex--;
            changeCalculationPhase(currentCalculateIndex);
        }

        private void updateNodes()
        {
            foreach (NodeElement nElem in nodeElements)
            {
                nElem.updateRenderChanges();
            }
        }

        private void changeCalculationPhase(int index)
        {
            dc.loadPhaseState(index);
            updateNodes();
        }

        private void generateButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                int nodesAmount = int.Parse(nodesTextBox.Text);
                int edgesAmount = int.Parse(edgesTextBox.Text);
                int costFrom = int.Parse(costFromTextBox.Text);
                int costTo = int.Parse(costToTextBox.Text);

                removeNodeElements();

                GraphGenerator gGenerator = new GraphGenerator();
                List<Node> genNodes = gGenerator.generate(nodesAmount, edgesAmount, costFrom, costTo);

                setCircularNodePositions(genNodes);

                createNodeElements(genNodes);
                createEdgeElements(genNodes);
                int sa = 02;
            } catch (FormatException except)
            {
                Console.WriteLine(except.Source);
            }
        }

        private void createEdgeElements(List<Node> nodes)
        {
            for (int i = 0; i < nodeElements.Count; i++)
            {
                List<Node> nl = new List<Node>(nodeElements[i].node.targets.Keys);
                for (int j = 0; j < nl.Count; j++) 
                {
                    nodeElements[i].addEdge( getNodeElementWithNode( nl[j]) );
                }
            }
        }

        private NodeElement getNodeElementWithNode(Node node)
        {
            foreach (NodeElement ne in nodeElements)
            {
                if (ne.node == node)
                {
                    return ne;
                }
            }
            return null;
        }


        private void setCircularNodePositions(List<Node> nodes)
        {
            float radious = 150.0f;
            System.Windows.Point middle = new System.Windows.Point(200,200);
            double jumpAngle = Math.PI * 2 / nodes.Count;

            for (int i=0; i<nodes.Count; i++)
            {
                double xoff = Math.Sin(jumpAngle * i);
                double yoff = Math.Cos(jumpAngle * i);

                nodes[i].position = new System.Windows.Point(xoff * radious + middle.X, yoff * radious + middle.Y);
            }

        }

        private void removeNodeElements()
        {
            while(nodeElements.Count > 0)
            {
                nodeElements[0].destroy();
            }
            nodeElements.Clear();
            Node.resetIDs();
        }

        private void createNodeElements(List<Node> nodes)
        {
            foreach(Node node in nodes)
            {
                NodeElement el = new NodeElement(node);
                el.addToCanvas(MainCanvas);
                nodeElements.Add(el);
            }
        }

        private void clearButtonClicked(object sender, RoutedEventArgs e)
        {
            removeNodeElements();
        }
    }
}
