using Lyzard.FileSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Lyzard.IDE.ViewModels
{
    public class FileExplorerViewModel : ExplorerViewModelBase
    {
        private bool _canHide = true;

        public FileExplorerViewModel()
        {
            ContentId = "FileExplorer";
            Title = "File Explorer";
            IconSource = new BitmapImage(new Uri($"pack://application:,,/Resources/Images/FileExplore.png"));
            Items = new ObservableCollection<DirectoryItemViewModel>(FileSystemAccess.GetDrives().ToList().Select( drive => {
                return new DirectoryItemViewModel(drive);
            })); 

        }

        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }
            


        public bool CanHide
        {
            get
            {
                return _canHide;
            }
            set
            {
                _canHide = value;
                OnPropertyChanged();
            }
        }

    }
}
