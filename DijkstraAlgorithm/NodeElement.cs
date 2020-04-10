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
        Node node;
        static int _nodeId = 0;
        Button model = null;

        public NodeElement(Point position)
        {
            node = new Node();
            node.id = _nodeId++;
            node.position = position;

            model = new Button();
            model.Click += nodeWasClicked;
            model.Width = 40;
            model.Height = 40;

            model.SetValue(Canvas.LeftProperty, position.X - model.Width/2);
            model.SetValue(Canvas.TopProperty, position.Y - model.Height/2);
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

        public void nodeWasClicked(object sender, RoutedEventArgs e)
        {
            if (Master.actualMode == Mode.ADD_EDGE)
            {
                Master.creatingEdge(this.node);
            } else if (Master.actualMode == Mode.REMOVE)
            {
                Master.removeNode(this);
            }
        }
    }
}
