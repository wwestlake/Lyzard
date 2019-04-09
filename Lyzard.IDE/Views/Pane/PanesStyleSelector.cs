using Lyzard.IDE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lyzard.IDE.Views.Pane
{
    class PanesStyleSelector : StyleSelector
    {
        public Style ExplorerStyle
        {
            get;
            set;
        }

        public Style FileStyle
        {
            get;
            set;
        }

        public Style ConsoleStyle
        {
            get;
            set;
        }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is ExplorerViewModelBase)
                return ExplorerStyle;

            if (item is DocumentViewModelBase)
                return FileStyle;

            if (item is ConsoleViewModelBase)
                return ConsoleStyle;

            return base.SelectStyle(item, container);
        }
    }
}
