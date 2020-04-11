using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DijkstraAlgorithm
{
    public class EdgeElement
    {
        Canvas canvas = null;
        public Edge edge { get; set; }

        System.Windows.Shapes.Line line = null;
        EdgeCostTextBox edgeCostTextBox = null;

        NodeElement fromNodeElement;
        NodeElement toNodeElement;


        public EdgeElement(NodeElement from, NodeElement to)
        {
            fromNodeElement = from;
            toNodeElement = to;
            edge = new Edge(from.node, to.node);
        }

        public void createLineSegment(Canvas canvas)
        {
            if (this.canvas == null)
            {
                this.canvas = canvas;
            }

            if (line == null)
            {
                line = new System.Windows.Shapes.Line();
                line.Stroke = System.Windows.Media.Brushes.Black;
                line.X1 = edge.fromNode.position.X;
                line.Y1 = edge.fromNode.position.Y;
                line.X2 = edge.toNode.position.X;
                line.Y2 = edge.toNode.position.Y;
                line.HorizontalAlignment = HorizontalAlignment.Left;
                line.VerticalAlignment = VerticalAlignment.Center;
                line.StrokeThickness = 2;

                edgeCostTextBox = new EdgeCostTextBox(this);
                edgeCostTextBox.SetValue(Canvas.LeftProperty, (line.X1 + line.X2) / 2);
                edgeCostTextBox.SetValue(Canvas.TopProperty, (line.Y1 + line.Y2) / 2);

                Canvas.SetZIndex(line, -1);
                Canvas.SetZIndex(edgeCostTextBox, -1);

                canvas.Children.Add(line);
                canvas.Children.Add(edgeCostTextBox);
            }
            else
            {
                removeLineSegment();
            }
        }

        private void removeLineSegment()
        {
            canvas.Children.Remove(line);
            canvas.Children.Remove(edgeCostTextBox);
            line = null;
            edgeCostTextBox = null;
        }

        public void setEdgeCost(int cost)
        {
            fromNodeElement.node.targets[toNodeElement.node] = cost;
            toNodeElement.node.targets[fromNodeElement.node] = cost;
            edge.cost = cost;
        }

        public void destroy()
        {
            fromNodeElement.removeEdge(this);
            toNodeElement.removeEdge(this);
            Master.removeEdge(edge.fromNode, edge.toNode);
            removeLineSegment();
        }
    }
}
