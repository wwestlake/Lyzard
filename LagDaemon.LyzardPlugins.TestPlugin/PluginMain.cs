using Lyzard.PluginFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Input;

namespace LagDaemon.LyzardPlugins.TestPlugin
{
    [Serializable]
    public class PluginMain : MarshalByRefObject, IPlugin
    {
        private IApplicationApi _api;

        public void Initialize(IApplicationApi api)
        {
            _api = api;
            var tab = _api.MainRibbon.AddTabToRibbon("My Plugin", this);
            var group = new RibbonGroup { Header = "Document" };
            var button = new RibbonButton() { Label = "New Document" };
            button.SetBinding(RibbonButton.CommandProperty, new Binding("CreateDocument"));
            group.Items.Add(button);
            tab.Items.Add(group);


            var pane = new PluginPane();
            api.CreateToolPane(pane, new TestPluginPaneViewModel(api));
        }

        public ObservableCollection<RibbonButton> Items { get; set; } = new ObservableCollection<RibbonButton>();

        public ICommand CreateDocument => new PluginDelegateCommand((x) => NewPluginDocument());

        private void NewPluginDocument()
        {
            _api.CreateDocument(new PluginDocument(), new TestPluginViewModel());
        }
    }
}
