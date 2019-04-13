using Lyzard.FileSystem;
using Lyzard.IDE.ViewModels;
using Lyzard.PluginFramework;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace Lyzard.IDE.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainRibbonApi
    {
        public static IMainRibbonApi MainWindowApi { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                MainWindowApi = this;
                DataContext = new MainWindowViewModel();
                //LoadLayout();
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


        public RibbonTab AddTabToRibbon(string header, object dataContext)
        {
            var tab = new RibbonTab() { Header = header, DataContext = dataContext, IsEnabled = true };
            MainRibbon.Items.Add(tab);
            return tab;
        }


    }

    

}
