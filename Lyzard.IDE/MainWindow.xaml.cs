using Lyzard.FileSystem;
using Lyzard.IDE.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace Lyzard.IDE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                DataContext = new MainWindowViewModel();
                LoadLayout();
            };

        }


        public void SaveLayout()
        {
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(_dockManager);
            using (var stream = File.OpenWrite(CommonFolders.LyzardLayout))
            {
                serializer.Serialize(stream);
            }
        }

        public void LoadLayout()
        {
            if (!File.Exists(CommonFolders.LyzardLayout)) return;
            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(_dockManager);
            layoutSerializer.LayoutSerializationCallback += LayoutSerializer_LayoutSerializationCallback;
            using (var reader = File.OpenRead(CommonFolders.LyzardLayout))
            {
                layoutSerializer.Deserialize(reader);
            }
        }

        private void LayoutSerializer_LayoutSerializationCallback(object sender, LayoutSerializationCallbackEventArgs e)
        {
            var o = e.Content;
        }

        private void RibbonMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveLayout();
        }

        private void RibbonMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            LoadLayout();
        }
    }
}
