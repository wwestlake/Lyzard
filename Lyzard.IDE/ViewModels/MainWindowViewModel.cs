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
using Lyzard.AppDominaControl;
using Lyzard.IDE.Dialogs;
using Lyzard.IDE.Messages;
using Lyzard.IDE.Views.Dialogs;
using Lyzard.Interfaces;
using Lyzard.MessageBus;
using Lyzard.PluginFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Xceed.Wpf.AvalonDock.Themes;

namespace Lyzard.IDE.ViewModels
{
    [Serializable]
    internal class MainWindowViewModel : ViewModelBase
    {

        private string _title;
        private Dictionary<string, Theme> _themes = new Dictionary<string, Theme>();
        private DockManagerViewModel _dockManager;


        public MainWindowViewModel()
        {
            DockManager = new DockManagerViewModel();
            Title = "Lyzard Developer";


            _themes.Add("Aero", new AeroTheme());
            _themes.Add("VS 2010", new VS2010Theme());
            _themes.Add("VS 2013 Blue", new Vs2013BlueTheme());
            _themes.Add("VS 2013 Dark", new Vs2013DarkTheme());
            _themes.Add("VS 2013 Light", new Vs2013LightTheme());
            _themes.Add("Metro", new MetroTheme());
            _themes.Add("Generic", new GenericTheme());
            _themes.Add("Expression Dark", new ExpressionDarkTheme());
            _themes.Add("Expression Light", new ExpressionLightTheme());
            _dockManager.CurrentTheme = _themes["VS 2013 Dark"];
            RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            DockManager._fileexpl.ToolWindowHidden += (s, e) => { DoToggleFileManager(); };

            DockManager.ActiveDocumentChanged += _dockManager_ActiveDocumentChanged;

            MessageBroker.Instance.Subscribe<FileSavedMessage>((msg) => {
                MessageBroker.Instance.Reply(this, msg, new FileSavedMessage() { Vm = null });
            });

        }

        private void _dockManager_ActiveDocumentChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("SaveCommand");
            OnPropertyChanged("SaveAsCommand");
            OnPropertyChanged("SaveAllCommand");
        }

        public string Title { get { return _title; } set { _title = value; OnPropertyChanged(); } }

        public ICommand NewProject  => new DelegateCommand((x) => {
            DockManager.CreateProject();
        });

        public IList<string> ThemeNames
        {
            get
            {
                return _themes.Keys.ToList();
            }
        }

        private string _selectedStyle = "VS 2013 Dark";
        public string SelectedStyle 
        {
            get
            {
                return _selectedStyle;
            }
            set
            {
                if (value == null) return;
                if (value == _selectedStyle) return;
                _selectedStyle = value;
                OnPropertyChanged();
                _dockManager.CurrentTheme = _themes[_selectedStyle];
            }

        }

        public DockManagerViewModel DockManager
        {
            get
            {
                return _dockManager;
            }
            set
            {
                _dockManager = value;
                OnPropertyChanged();
            }
        }

        private string _toggleFileManagerHeader = "Hide File Manager";

        public string ToggleFileManagerHeader
        {
            get
            {
                return _toggleFileManagerHeader;
            }
            set
            {
                _toggleFileManagerHeader = value;
                OnPropertyChanged();
            }
        }

        public ICommand FileNew => new DelegateCommand((x) =>
        {
            _dockManager.Documents.Add(new CodeEditorViewModel());
        });

        public ICommand NewDiagram => new DelegateCommand((x) => {
            _dockManager.Documents.Add(new DiagramViewModel() { Title = "New Diagram" } );

        });

        public ICommand OpenCodeFile => new DelegateCommand((x) => {
            var file = DialogManager.OpenFile();
            if (file != null)
            {
                DockManager.Documents.Add(new CodeEditorViewModel(file));
            }
        });

        public ICommand OpenAudioFile => new DelegateCommand((x) => {
            var file = DialogManager.OpenFilePath();
            if (file != null)
            {
                DockManager.Documents.Add(new AudioFileViewModel(file));
            }
        });


        public bool FileExplorerVisibility
        {
            get { return DockManager._fileexpl.IsVisible; }
            set
            {
                if (value)
                    DockManager._fileexpl.IsVisible = true;
                else
                    DockManager._fileexpl.IsVisible = false;
                OnPropertyChanged();
            }
        }

        public bool ProjectExplorerVisibility
        {
            get { return DockManager._project.IsVisible; }
            set
            {
                if (value)
                    DockManager._project.IsVisible = true;
                else
                    DockManager._project.IsVisible = false;
                OnPropertyChanged();
            }
        }

        public bool PropertiesExplorerVisibility
        {
            get { return DockManager._properties.IsVisible; }
            set
            {
                if (value)
                    DockManager._properties.IsVisible = true;
                else
                    DockManager._properties.IsVisible = false;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand => new DelegateCommand((x) =>
        {
            if (_dockManager.ActiveDocument != null)
                _dockManager.ActiveDocument.Save(x);
            OnPropertyChanged();
        }, (x) => {
            return _dockManager.ActiveDocument != null ? _dockManager.ActiveDocument.CanSave(x) : false;
        });

        public ICommand SaveAsCommand => new DelegateCommand((x) =>
        {
            if (_dockManager.ActiveDocument != null)
                _dockManager.ActiveDocument.SaveAs(x);
            OnPropertyChanged();
        }, (x) => _dockManager.ActiveDocument != null);

        public ICommand SaveAllCommand => new DelegateCommand((x) =>
        {
            _dockManager.SaveAll(x);
            OnPropertyChanged();
        }, (x) =>  _dockManager.CanSaveAll(x) );



        public void DoToggleFileManager()
        {
            if (!_dockManager.Anchorables.Contains(DockManager._fileexpl))
            {
                _dockManager.Anchorables.Add(DockManager._fileexpl);
            }
            DockManager._fileexpl.IsVisible = !DockManager._fileexpl.IsVisible;
            ToggleFileManagerHeader =
                DockManager._fileexpl.IsVisible ? "Hide File Explorer" : "Show File Explorer";

        }

        public ICommand TestLoadPlugin => new DelegateCommand((x) =>
        {
            var loader = new AppDomainLoader(@"E:\Software Development\Visual Studio 2017\Projects\Lyzard\LagDaemon.LyzardPlugins.TestPlugin\bin\Debug\LagDaemon.LyzardPlugins.TestPlugin.dll");
            var plugin = loader.RunRemoteFunc(() => 
            {
                var pluginLoader = new PluginLoader();
                return pluginLoader.GetPlugin();
            });
            plugin.Initialize(_dockManager);
        });


    }
}
