using Lyzard.PluginFramework;
using System;
using System.Collections.Generic;
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

namespace LagDaemon.LyzardPlugins.TestPlugin
{
    /// <summary>
    /// Interaction logic for PluginDocument.xaml
    /// </summary>
    public partial class PluginDocument : UserControl, IPluginDocumentView
    {
        public PluginDocument()
        {
            InitializeComponent();
        }

        public IPluginDocumentViewModel ViewModel { get => DataContext as IPluginDocumentViewModel; set => DataContext = value; }
    }
}
