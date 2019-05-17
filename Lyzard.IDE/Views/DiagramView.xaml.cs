using Lyzard.CustomControls;
using Lyzard.IDE.ViewModels;
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
                if (item.Content is Grid)
                {
                    var path = (item.Content as Grid).FindName("Path") as Path;
                    var result = SimulationViewModelSelector.SelectViewAndViewModel(path);
                    if (result != null) AssignSimulationViewModeAndView(item, result);
                    return;
                }
                else
                {
                    var path = item.Content as Path;
                    var result = FlowChartViewModelSelector.SelectViewAndViewModel(path);
                    if (result != null) AssignFlowChartViewModeAndView(item, result);
                }
            };
        }

        private static void AssignSimulationViewModeAndView(DesignerItem item, Tuple<ViewModels.ViewModelBase, UserControl> result)
        {
            if (result != null)
            {
                result.Item2.DataContext = result.Item1;
                item.Content = result.Item2;
                item.UpdateLayout();
            }
        }
        private static void AssignFlowChartViewModeAndView(DesignerItem item, Tuple<ViewModels.ViewModelBase, UserControl> result)
        {
            if (result != null)
            {
                result.Item2.DataContext = result.Item1;
                var path = item.Content;
                item.Content = result.Item2;
                (result.Item2.FindName("Content") as ContentControl).Content = path;
                item.UpdateLayout();
                item.Selected += Item_Selected;
            }
        }

        private static void Item_Selected(object sender, EventArgs args)
        {
            var item = sender as DesignerItem;
            if (item != null)
            {
                var vm = item.DataContext;
                DockManagerViewModel.DocumentManager.SelectDiagramItem(vm);
            }
        }
    }
}
