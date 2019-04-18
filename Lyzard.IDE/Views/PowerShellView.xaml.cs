using Lyzard.Config;
using Lyzard.FileSystem;
using Lyzard.IDE.ViewModels;
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

namespace Lyzard.IDE.Views
{
    /// <summary>
    /// Interaction logic for ConsoleView.xaml
    /// </summary>
    public partial class PowerShellView : UserControl
    {
        public PowerShellView()
        {
            InitializeComponent();
            Console.FontSize = StateManager.SystemState.PowerShellConsoleFontSize;
            var startFolder = CommonFolders.UserProjects;
            Console.StartProcess("powershell.exe", $" -noexit -command \"cd {startFolder}\"");
        }

        private void Console_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                Console.FontSize += Math.Sign(e.Delta);
                if (Console.FontSize > 34) Console.FontSize = 34;
                if (Console.FontSize < 8) Console.FontSize = 8;
                StateManager.SystemState.PowerShellConsoleFontSize = Console.FontSize;
                e.Handled = true;
            }
        }


    }
}
