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
        }

        private void calculateButtonClick(object sender, RoutedEventArgs e)
        {
            DijkstraCalculations dc = new DijkstraCalculations();
            dc.calculate();
        }
    }
}
