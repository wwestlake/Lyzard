/* 
 * Lyzard Modeling and Simulation System
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
