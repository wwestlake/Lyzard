using Lyzard.IDE.UserControls.DiagramControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    public static class SimulationViewModelSelector
    {
        public static Tuple<ViewModelBase, UserControl> SelectViewAndViewModel(Path item)
        {
            if (item == null) return null;
            if (item.Tag == null) return null;
            if (item.Tag.ToString() == "Function") return new Tuple<ViewModelBase, UserControl>(new SimFunctionViewModel(), new FunctionControl());

            return null;
        }
    }
}
