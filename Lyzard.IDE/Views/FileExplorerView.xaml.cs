using Lyzard.IDE.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lyzard.IDE.Views
{
    /// <summary>
    /// Interaction logic for FileExplorerView.xaml
    /// </summary>
    public partial class FileExplorerView : UserControl
    {
        public FileExplorerView()
        {
            InitializeComponent();
        }

        private void FolderView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var view = e.Source as TreeView;
            if (view != null)
            {
                var item = view.SelectedItem as DirectoryItemViewModel;
                if (item != null)
                {
                    item.DoubleClick(item);
                }
            }
        }
    }
}
