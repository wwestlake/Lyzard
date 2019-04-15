using Lyzard.Config;
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
        internal static MainWindow CurrentWindow { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                MainWindowApi = this;
                CurrentWindow = this;
                DataContext = new MainWindowViewModel();
                LoadLayout();
            };

            Closing += (s, e) =>
            {
                SaveLayout();
                StateManager.SaveState();
            };

        }


        public void SaveLayout()
        {
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(_dockManager);
            using (var stream = new StringWriter())
            {
                serializer.Serialize(stream);
                StateManager.SystemState.Layout = stream.ToString();
            }
        }

        public void LoadLayout()
        {
            if (string.IsNullOrEmpty(StateManager.SystemState.Layout)) return;
            XmlLayoutSerializer layoutSerializer = new XmlLayoutSerializer(_dockManager);
            layoutSerializer.LayoutSerializationCallback += LayoutSerializer_LayoutSerializationCallback;
            using (var reader = new StringReader(StateManager.SystemState.Layout))
            {
                layoutSerializer.Deserialize(reader);
            }
        }

        private void LayoutSerializer_LayoutSerializationCallback(object sender, LayoutSerializationCallbackEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;

            switch (e.Model.ContentId)
            {
                case "FileExplorer":
                    e.Content = vm.DockManager._fileexpl;
                    break;
                case "CommandConsole":
                    e.Content = vm.DockManager._console;
                    break;
                case "Output":
                    e.Content = vm.DockManager._output;
                    break;
                case "Project":
                    e.Content = vm.DockManager._project;
                    break;
                case "Properties":
                    e.Content = vm.DockManager._properties;
                    break;
                default:
                    if (e.Model.ContentId == null)
                    {
                        e.Cancel = true;
                        break;
                    }
                    if (e.Model.ContentId.StartsWith("file://"))
                    {
                        var path = e.Model.ContentId.Substring(7);
                        e.Content = new CodeEditorViewModel(path);
                        DockManagerViewModel.DocumentManager.Documents.Add(e.Content as CodeEditorViewModel);
                    } else
                    {
                        e.Cancel = true;
                    }
                    break;
            }
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
