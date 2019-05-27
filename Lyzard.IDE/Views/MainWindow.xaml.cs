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
using Lyzard.Config;
using Lyzard.FileSystem;
using Lyzard.IDE.ViewModels;
using Lyzard.PluginFramework;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace Lyzard.IDE.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow : Window, IMainRibbonApi
    {
        public static IMainRibbonApi MainWindowApi { get; private set; }
        internal static MainWindow CurrentWindow { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                MainWindowApi = this;
                CurrentWindow = this;
                DataContext = new MainWindowViewModel();
                LoadLayout();
            };

            Closing += (s, e) =>
            {
                SaveLayout();
                StateManager.SaveState();
            };

        }


        public void SaveLayout()
        {
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(_dockManager);
            using (var stream = new StringWriter())
            {
                serializer.Serialize(stream);
                StateManager.SystemState.Layout = stream.ToString();
                StateManager.SaveState();

            }
        }

        public void LoadLayout()
        {
            if (string.IsNullOrEmpty(StateManager.SystemState.Layout)) return;
            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(_dockManager);
            layoutSerializer.LayoutSerializationCallback += LayoutSerializer_LayoutSerializationCallback;
            using (var reader = new StringReader(StateManager.SystemState.Layout))
            {
                layoutSerializer.Deserialize(reader);
            }
        }

        private void LayoutSerializer_LayoutSerializationCallback(object sender, LayoutSerializationCallbackEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;

            switch (e.Model.ContentId)
            {
                case "FileExplorer":
                    e.Content = vm.DockManager._fileexpl;
                    break;
                case "CommandConsole":
                    e.Content = vm.DockManager._console;
                    break;
                case "PowerShell":
                    e.Content = vm.DockManager._powerShell;
                    break;
                case "Output":
                    e.Content = vm.DockManager._output;
                    break;
                case "Project":
                    e.Content = vm.DockManager._project;
                    break;
                case "Properties":
                    e.Content = vm.DockManager._properties;
                    break;
                default:
                    if (e.Model.ContentId == null)
                    {
                        e.Cancel = true;
                        break;
                    }
                    if (e.Model.ContentId.StartsWith("file://"))
                    {
                        var path = e.Model.ContentId.Substring(7);
                        e.Content = new CodeEditorViewModel(path);
                        DockManagerViewModel.DocumentManager.Documents.Add(e.Content as CodeEditorViewModel);
                    } else
                    {
                        e.Cancel = true;
                    }
                    break;
            }
        }

        private void RibbonMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveLayout();
        }

        private void RibbonMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            LoadLayout();
        }


        public RibbonTab AddTabToRibbon(string header, object dataContext)
        {
            var tab = new RibbonTab() { Header = header, DataContext = dataContext, IsEnabled = true };
            MainRibbon.Items.Add(tab);
            return tab;
        }


    }

    

}
