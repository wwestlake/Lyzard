using Lyzard.Interfaces;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Executive
{
    public class PluginWrapper
    {
        private readonly AppDomain _appDomain;
        private readonly IPlugin _plugin;
        private readonly string _path;
        private readonly string _dllname;
        private readonly CompilerResults _results;

        public PluginWrapper(string dllname, string pathToAssembly, string startClassName)
        {
            _path = pathToAssembly;
            _dllname = dllname;
            _appDomain = CreateAppDomain(_dllname);
            
            

            Plugin = InstantiatePlugin(_dllname, startClassName);
        }
         
        

        public IPlugin Plugin { get; private set; }
       

        private AppDomain CreateAppDomain(string dllname)
        {
            var setup = new AppDomainSetup
            {
                ApplicationName = dllname,
                ConfigurationFile = $"{dllname}.config",
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory
            };
            var appDomain = AppDomain.CreateDomain(
                setup.ApplicationName,
                AppDomain.CurrentDomain.Evidence,
                setup
                );
            return appDomain;
        }

        private IPlugin InstantiatePlugin(string dllname, string typeName)
        {
            var plugin = _appDomain.CreateInstanceFromAndUnwrap(dllname, typeName) as IPlugin;
            return plugin;
        }

        public void Unload()
        {
            AppDomain.Unload(_appDomain);
        }

    }
}
