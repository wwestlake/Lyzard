using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Themes;

namespace Lyzard.IDE.ViewModels
{
    public class DockManagerViewModel : ViewModelBase
    {
        private ObservableCollection<DocumentViewModelBase> _editors;
        private ObservableCollection<PaneViewModel> _tools;
        private Theme _theme;

        public DockManagerViewModel()
        {
            _editors = new ObservableCollection<DocumentViewModelBase>();
            _tools = new ObservableCollection<PaneViewModel>();
        }

        public ObservableCollection<DocumentViewModelBase> Documents
        {
            get { return _editors; }
            set { _editors = value; FirePropertyChanged(); }
        }

        public ObservableCollection<PaneViewModel> Anchorables
        {
            get { return _tools; }
            set { _tools = value; FirePropertyChanged(); }
        }

        public Theme CurrentTheme
        {
            get
            {
                return _theme;
            }
            set
            {
                if (_theme != value)
                    _theme = value;
                FirePropertyChanged();

            }
        }

        DocumentViewModelBase _activeDocument;

        public DocumentViewModelBase ActiveDocument
        {
            get
            {
                return _activeDocument;
            }
            set
            {
                if (_activeDocument != value)
                {
                    _activeDocument = value;
                    FirePropertyChanged();
                    ActiveDocumentChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler ActiveDocumentChanged;

        internal void SaveAll(object x)
        {
        }

        internal bool CanSaveAll(object x)
        {
            return _editors.Any(editor => editor.IsDirty);
        }
    }
}
