using Lyzard.IDE.Messages;
using Lyzard.MessageBus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Lyzard.IDE.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private string _title;
        private DockManagerViewModel _dockManager;

        private readonly ConsoleViewModel _console = new ConsoleViewModel() { Title = "Console" };
        private readonly FileExplorerViewModel _fileexpl = new FileExplorerViewModel() { Title = "File Explorer" };
        private readonly OutputViewModel _output = new OutputViewModel() { Title = "Output" };
        private readonly ProjectExplorerViewModel _project = new ProjectExplorerViewModel() { Title = "Project Explorer" };
        private readonly PropertiesViewModel _properties = new PropertiesViewModel() { Title = "Properties" };

        public MainWindowViewModel()
        {
            _dockManager = new DockManagerViewModel();
            Title = "Lyzard Developer";

            RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            _fileexpl.ToolWindowHidden += (s, e) => { DoToggleFileManager(); };

            _dockManager.ActiveDocumentChanged += _dockManager_ActiveDocumentChanged;
        }

        private void _dockManager_ActiveDocumentChanged(object sender, EventArgs e)
        {
            FirePropertyChanged("SaveCommand");
            FirePropertyChanged("SaveAsCommand");
            FirePropertyChanged("SaveAllCommand");
        }

        public string Title { get { return _title; } set { _title = value; FirePropertyChanged(); } }

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

        private string _toggleFileManagerHeader = "Show File Manager";

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

        public ICommand ToggleFileManager => new DelegateCommand((x) => DoToggleFileManager());

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
            if (!_dockManager.Tools.Contains(_fileexpl))
            {
                _dockManager.Tools.Add(_fileexpl);
            }
            _fileexpl.IsVisible = !_fileexpl.IsVisible;
            ToggleFileManagerHeader =
                _fileexpl.IsVisible ? "Hide File Explorer" : "Show File Explorer";

        }


    }
}
