using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Lyzard.IDE.ViewModels
{
    public class FileExplorerViewModel : ToolViewModelBase
    {
        private bool _canHide = true;

        public FileExplorerViewModel()
        { 
            IconSource = new BitmapImage(new Uri($"pack://application:,,/Resources/Images/FileExplore.png"));
        }



        public bool CanHide
        {
            get
            {
                return _canHide;
            }
            set
            {
                _canHide = value;
                FirePropertyChanged();
            }
        }

    }
}
