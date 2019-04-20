using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lyzard.FileSystem;

namespace Lyzard.IDE.ViewModels
{
    public class DirectoryItemViewModel : ViewModelBase
    {
        private bool isSelected;
        private FileSystemItem item;

        public DirectoryItemViewModel(FileSystemItem item)
        {
            Item = item;
            ClearChildren();
        }

        public FileSystemItem Item { get => item; set { item = value; FirePropertyChanged(); } }

        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        public bool CanExpand { get { return Item.ItemType != FileSystemItemType.File; } }

        public ICommand ExpandCommand => new DelegateCommand(x => Expand(), x => true);

        public string ImageName
        {
            get
            {
                var filename = "";
                switch (Item.ItemType)
                {
                    case FileSystemItemType.Drive:
                        filename = "HardDrive.png";
                        break;
                    case FileSystemItemType.Folder:
                        filename = IsExpanded ? "Folder-Open.png" : "Folder-Closed.png";
                        break;
                    case FileSystemItemType.File:
                        var ext = System.IO.Path.GetExtension(Item.Name);
                        if (ext.StartsWith(".")) ext = ext.Substring(1);
                        filename = $"FileTypes/{ext}.png";
                        break;
                }
                return filename;
            }
        }


        public bool IsExpanded
        {
            get
            {
                return Children?.Count(f => f != null) > 0;
            }
            set
            {
                if (value == true)
                {
                    Children = Expand().Result;
                    Item.IsExpanded = true;
                }
                else
                {
                    ClearChildren();
                    Item.IsExpanded = false;
                }
                FirePropertyChanged();
                FirePropertyChanged("Children");
            }
        }

        public bool IsSelected { get => isSelected; set { isSelected = value; FirePropertyChanged(); } }


        private void ClearChildren()
        {
            Children = new ObservableCollection<DirectoryItemViewModel>();
            if (Item.ItemType != FileSystemItemType.File)
            {
                Children.Add(null);
            }
        }

        private async Task<ObservableCollection<DirectoryItemViewModel>> Expand()
        {
            return Task.Factory.StartNew(() =>
            {
                if (Item.ItemType == FileSystemItemType.File) return null;
                return new ObservableCollection<DirectoryItemViewModel>(
                    FileSystemAccess.GetFolderContents(Item.FullPath).ToList().Select(item =>
                        new DirectoryItemViewModel(item)
                    )
                );
            }).Result;
        }

        public void DoubleClick(DirectoryItemViewModel item)
        {
            DockManagerViewModel.DocumentManager.OpenFile(item.Item.FullPath);
        }
    }
}
