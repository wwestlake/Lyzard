using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.IDE.ViewModels.DialogsViewModels
{
    public class DialogViewModelBase : ViewModelBase
    {
        internal Action<DialogViewModelBase> Completed { get; set; }

    }
}
