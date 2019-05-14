using Lyzard.CustomControls;
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
                var item = s as DesignerItem;
                if (! (item.Content is Grid)) return;
                var path = (item.Content as Grid).FindName("Path") as Path;
                var result = SimulationViewModelSelector.SelectViewAndViewModel(path);
                if (result != null)
                {
                    result.Item2.DataContext = result.Item1;
                    item.Content = result.Item2;
                    item.UpdateLayout();
                }
            };
        }
    }
}
