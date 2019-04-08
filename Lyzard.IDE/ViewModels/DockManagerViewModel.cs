using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lyzard.IDE.ViewModels
{
    public class DockManagerViewModel : ViewModelBase
    {
        private ObservableCollection<CodeEditorViewModel> _editors;
        private ObservableCollection<ToolViewModelBase> _tools;

        public DockManagerViewModel()
        {
            _editors = new ObservableCollection<CodeEditorViewModel>();
            _tools = new ObservableCollection<ToolViewModelBase>();
        }

        public ObservableCollection<CodeEditorViewModel> Documents
        {
            get { return _editors; }
            set { _editors = value; FirePropertyChanged(); }
        }

        public ObservableCollection<ToolViewModelBase> Tools
        {
            get { return _tools; }
            set { _tools = value; FirePropertyChanged(); }
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
