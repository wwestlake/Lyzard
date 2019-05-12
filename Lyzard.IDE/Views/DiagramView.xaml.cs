using Lyzard.IDE.ViewModels.SimulationItemViewModels;
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

namespace Lyzard.IDE.Views
{
    /// <summary>
    /// Interaction logic for DiagramView.xaml
    /// </summary>
    public partial class DiagramView : UserControl
    {
        public DiagramView()
        {
            InitializeComponent();
            Designer.DropEvent += (s, e) =>
            {
                var grid = s as Grid;
                var path = grid.FindName("Path") as Path;
                var vm = SimulationViewModelSelector.SelectViewModel(path);
                if (vm != null)
                {
                    (grid.FindName("Title") as TextBlock).SetBinding(TextBlock.TextProperty, new Binding("Title"));
                    var inputs = (grid.FindName("Input") as ListView);
                    inputs.SetBinding(ListView.ItemsSourceProperty, new Binding("Inputs"));
                    var outputs = (grid.FindName("Output") as ListView);
                    outputs.SetBinding(ListView.ItemsSourceProperty, new Binding("Outputs"));
                    grid.DataContext = vm;
                    grid.UpdateLayout();
                }
            };
        }
    }
}
