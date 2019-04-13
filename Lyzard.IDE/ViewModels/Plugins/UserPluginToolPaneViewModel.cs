using Lyzard.PluginFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.IDE.ViewModels.Plugins
{
    public class UserPluginToolPaneViewModel : ExplorerViewModelBase
    {
        private IPluginToolPaneView _userControl;
        private IPluginToolPaneViewModel _viewModel;

        public new string Title { get { return _viewModel.Title; } set { } }

        public IPluginToolPaneView Content
        {
            get { return _userControl; }
            set
            {
                _userControl = value;
                FirePropertyChanged();
            }
        }

        public IPluginToolPaneViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                FirePropertyChanged();
            }
        }
    }
}
