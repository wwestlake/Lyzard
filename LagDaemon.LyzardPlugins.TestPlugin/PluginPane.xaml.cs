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
    /// Interaction logic for PluginPane.xaml
    /// </summary>
    public partial class PluginPane : UserControl, IPluginToolPaneView
    {
        public PluginPane()
        {
            InitializeComponent();
            
        }

        public IPluginToolPaneViewModel ViewModel { get => DataContext as IPluginToolPaneViewModel; set { this.DataContext = value; } }
        
        public Style GetStyle(string key)
        {
            return FindResource(key) as Style;
        }
    }
}
