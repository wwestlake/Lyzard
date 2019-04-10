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

        public DocumentViewModelBase()
        {
            _filePath = "Here is a tooltip";
            IsSelected = true;
        }

        public ICommand CloseCommand => new DelegateCommand((x) => {
            Close();
        });

        public bool IsDirty { get; set; } = true;

        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                FirePropertyChanged();
            }
        }

        public abstract bool CanSave(object param);
        public abstract void Save(object param);
        public abstract void SaveAs(object param);
        public abstract void Close();

    }
}
