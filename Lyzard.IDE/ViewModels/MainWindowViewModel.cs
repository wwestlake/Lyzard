using Lyzard.AppDominaControl;
using Lyzard.IDE.Messages;
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
using System.Windows.Input;
using System.Windows.Threading;
using Xceed.Wpf.AvalonDock.Themes;

namespace Lyzard.IDE.ViewModels
{
    [Serializable]
    public class MainWindowViewModel : ViewModelBase
    {

        private string _title;
        private Dictionary<string, Theme> _themes = new Dictionary<string, Theme>();
        private DockManagerViewModel _dockManager;

        private readonly CommandConsoleViewModel _console = new CommandConsoleViewModel() { Title = "Console" };
        private readonly FileExplorerViewModel _fileexpl = new FileExplorerViewModel() { Title = "File Explorer" };
        private readonly OutputViewModel _output = new OutputViewModel() { Title = "Output" };
        private readonly ProjectExplorerViewModel _project = new ProjectExplorerViewModel() { Title = "Project Explorer" };
        private readonly PropertiesViewModel _properties = new PropertiesViewModel() { Title = "Properties" };

        public MainWindowViewModel()
        {
            DockManager = new DockManagerViewModel();
            Title = "Lyzard Developer";

            DockManager.Anchorables.Add(_console);
            DockManager.Anchorables.Add(_fileexpl);
            DockManager.Anchorables.Add(_output);
            DockManager.Anchorables.Add(_project);
            DockManager.Anchorables.Add(_properties);

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
            _fileexpl.ToolWindowHidden += (s, e) => { DoToggleFileManager(); };

            _dockManager.ActiveDocumentChanged += _dockManager_ActiveDocumentChanged;

            MessageBroker.Instance.Subscribe<FileSavedMessage>((msg) => {
                MessageBroker.Instance.Reply(this, msg, new FileSavedMessage() { Vm = null });
            });

        }

        private void _dockManager_ActiveDocumentChanged(object sender, EventArgs e)
        {
            FirePropertyChanged("SaveCommand");
            FirePropertyChanged("SaveAsCommand");
            FirePropertyChanged("SaveAllCommand");
        }

        public string Title { get { return _title; } set { _title = value; FirePropertyChanged(); } }


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
                FirePropertyChanged();
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
                FirePropertyChanged();
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
                FirePropertyChanged();
            }
        }

        public ICommand FileNew => new DelegateCommand((x) =>
        {
            _dockManager.Documents.Add(new CodeEditorViewModel());
            //FirePropertyChanged("SaveCommand");
        });

        public bool FileExplorerVisibility
        {
            get { return _fileexpl.IsVisible; }
            set
            {
                if (value)
                    _fileexpl.IsVisible = true;
                else
                    _fileexpl.IsVisible = false;
                FirePropertyChanged();
            }
        }

        public bool ProjectExplorerVisibility
        {
            get { return _project.IsVisible; }
            set
            {
                if (value)
                    _project.IsVisible = true;
                else
                    _project.IsVisible = false;
                FirePropertyChanged();
            }
        }

        public bool PropertiesExplorerVisibility
        {
            get { return _properties.IsVisible; }
            set
            {
                if (value)
                    _properties.IsVisible = true;
                else
                    _properties.IsVisible = false;
                FirePropertyChanged();
            }
        }

        public ICommand SaveCommand => new DelegateCommand((x) =>
        {
            if (_dockManager.ActiveDocument != null)
                _dockManager.ActiveDocument.Save(x);
            FirePropertyChanged();
        }, (x) => {
            return _dockManager.ActiveDocument != null ? _dockManager.ActiveDocument.CanSave(x) : false;
        });

        public ICommand SaveAsCommand => new DelegateCommand((x) =>
        {
            if (_dockManager.ActiveDocument != null)
                _dockManager.ActiveDocument.SaveAs(x);
            FirePropertyChanged();
        }, (x) => _dockManager.ActiveDocument != null);

        public ICommand SaveAllCommand => new DelegateCommand((x) =>
        {
            _dockManager.SaveAll(x);
            FirePropertyChanged();
        }, (x) =>  _dockManager.CanSaveAll(x) );



        public void DoToggleFileManager()
        {
            if (!_dockManager.Anchorables.Contains(_fileexpl))
            {
                _dockManager.Anchorables.Add(_fileexpl);
            }
            _fileexpl.IsVisible = !_fileexpl.IsVisible;
            ToggleFileManagerHeader =
                _fileexpl.IsVisible ? "Hide File Explorer" : "Show File Explorer";

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
