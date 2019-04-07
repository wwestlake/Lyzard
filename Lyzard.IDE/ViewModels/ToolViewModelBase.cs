using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lyzard.IDE.ViewModels
{
    public delegate void ToolWindowHiddenEventHandler(object sender, EventArgs e);

    public class ToolViewModelBase : PaneViewModel 
    {
        private string _title;
        private bool _isVisible = false;

        public event ToolWindowHiddenEventHandler ToolWindowHidden;

        public ToolViewModelBase()
        {

        }


        protected void OnToolWindowHidden()
        {
            ToolWindowHidden?.Invoke(this, new EventArgs());
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
                FirePropertyChanged();
            }
        }

        public ICommand HideCommand => new DelegateCommand((x) => {
            OnToolWindowHidden();
        });


    }
}
