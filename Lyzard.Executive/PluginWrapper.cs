/* 
 * Lyzard Code Generation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Lyzard.Interfaces;
using Lyzard.PluginFramework;
using System;
using System.CodeDom.Compiler;

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
