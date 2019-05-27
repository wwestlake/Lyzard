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
