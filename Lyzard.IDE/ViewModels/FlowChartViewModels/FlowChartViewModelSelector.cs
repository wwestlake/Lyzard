using Lyzard.IDE.UserControls.DiagramControls;
using Lyzard.IDE.ViewModels.FlowChartViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    public static class FlowChartViewModelSelector
    {
        public static Tuple<ViewModelBase, UserControl> SelectViewAndViewModel(Path item)
        {
            if (item == null) return null;
            return new Tuple<ViewModelBase, UserControl>(new FlowChartProcessViewModel(item.Tag), new FlowChartProcessControl());
        }
    }
}
