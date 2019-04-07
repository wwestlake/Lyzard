using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lyzard.IDE.ViewModels
{
    public class DocumentViewModelBase : PaneViewModel
    {
        private string _title;
        private string _filePath;

        public DocumentViewModelBase()
        {
            _filePath = "Here is a tooltip";
        }

        public ICommand CloseCommand => new DelegateCommand((x) => {
            var a = x;
        });


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
    }
}
