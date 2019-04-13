using Lyzard.PluginFramework;
using System.Windows.Input;

namespace LagDaemon.LyzardPlugins.TestPlugin
{
    public class TestPluginPaneViewModel : ViewModelBase, IPluginToolPaneViewModel
    {
        private IApplicationApi _api;

        public TestPluginPaneViewModel(IApplicationApi api)
        {
            _api = api;
        }

        public bool CanClose { get => false; set { } }

        public string Title { get => "Plugin Tool"; set { } }



    }
}