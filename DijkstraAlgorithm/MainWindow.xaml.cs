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
                NodeElement node = new NodeElement(e.GetPosition(MainCanvas));
                Master.addNode(node);
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

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as TextBox).Focusable = false;
                (sender as TextBox).CaretBrush = System.Windows.Media.Brushes.Transparent;
            }
        }

        private void textBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (sender as TextBox).Focusable = true;
            (sender as TextBox).CaretBrush = System.Windows.Media.Brushes.Black;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void calculateButtonClick(object sender, RoutedEventArgs e)
        {
            DijkstraCalculations dc = new DijkstraCalculations();
            dc.calculate();
        }
    }
}
