using Lyzard.PluginFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.PluginFramework
{
    public sealed class PluginLoader : MarshalByRefObject
    {

        public IPlugin GetPlugin()
        {
            var ad = AppDomain.CurrentDomain.SetupInformation.ApplicationName;
            var type = typeof(IPlugin);
            var result = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.GetInterfaces().Contains(type)).FirstOrDefault();
            return Activator.CreateInstance(result) as IPlugin;
        }

    }
}
