using Lyzard.PluginFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.LyzardPlugins.TestPlugin
{
    public class TestPluginViewModel : ViewModelBase, IPluginDocumentViewModel
    {
        public string MyProperty { get; set; } = "This is my property";
        public bool CanClose { get => false; set { } }
        public string Title { get => "Plugin Document"; set { } }

        public bool CanSave(object param)
        {
            return true;
        }

        public void Close()
        {
        }

        public void Save(object param)
        {
        }

        public void SaveAs(object param)
        {
        }
    }
}
