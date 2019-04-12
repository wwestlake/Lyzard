using Lyzard.PluginFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.LyzardPlugins.TestPlugin
{
    public class TestPluginViewModel : ViewModelBase
    {
        public string MyProperty { get; set; } = "This is my property";
        public override string Title { get => "My Plugin"; set { } }
        public override bool CanClose { get => true; set { } }

        public override bool CanSave(object param)
        {
            return false;
        }

        public override void Close()
        {
        }

        public override void Save(object param)
        {
        }

        public override void SaveAs(object param)
        {
        }
    }
}
