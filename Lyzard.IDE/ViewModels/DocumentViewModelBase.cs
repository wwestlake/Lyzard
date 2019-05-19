using Lyzard.IDE.Messages;
using Lyzard.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lyzard.IDE.ViewModels
{
    public abstract class DocumentViewModelBase : PaneViewModel
    {
        private string _filePath;
        protected bool initialLoad = false;
        private bool _isDirty;

        public DocumentViewModelBase()
        {
            _filePath = "Here is a tooltip";
            IsSelected = true;
        }

        public ICommand CloseCommand => new DelegateCommand((x) => {
            Close();
        });

        public bool IsDirty
        {
            get { return _isDirty; }
            set {
                if (!initialLoad)
                    _isDirty = value;
                else
                    initialLoad = false;
            }
        }

        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        public abstract bool CanSave(object param);
        public abstract void Save(object param);
        public abstract void SaveAs(object param);
        public abstract void Close();

    }
}
