using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lyzard.IDE.ViewModels
{

    public class ToolViewModelBase : PaneViewModel 
    {
        private string _title;
        private bool _isVisible = false;


        public ToolViewModelBase()
        {

        }



        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand HideCommand => new DelegateCommand((x) => {
            OnToolWindowHidden();
        });


    }
}
