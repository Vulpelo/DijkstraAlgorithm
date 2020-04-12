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
    /// Interaction logic for EdgeCost.xaml
    /// </summary>
    public partial class EdgeCostTextBox : UserControl
    {
        public EdgeElement edge { get; set; }
        public EdgeCostTextBox(EdgeElement edge)
        {
            this.edge = edge;
            InitializeComponent();
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
            TextBox tb = (TextBox)sender;
            edge.setEdgeCost(
                tb.Text.Length == 0 ? 
                0 : int.Parse(tb.Text) 
                );
        }

        private void costTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Master.actualMode == Mode.REMOVE)
            {
                edge.destroy();
            }
        }
    }
}
