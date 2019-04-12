using Lyzard.PluginFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.LyzardPlugins.TestPlugin
{
    [Serializable]
    public class PluginMain : MarshalByRefObject, IPlugin
    {
        public void Initialize(IApplicationApi api)
        {
            api.CreateDocument(new PluginDocument(), new TestPluginViewModel() );
        }
    }
}
