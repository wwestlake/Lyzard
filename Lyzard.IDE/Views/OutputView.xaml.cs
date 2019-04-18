using Lyzard.Config;
using Lyzard.SystemIO;
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
    /// Interaction logic for OutputView.xaml
    /// </summary>
    public partial class OutputView : UserControl, IOutputConsole
    {
        public OutputView()
        {
            InitializeComponent();
            Output.FontSize = StateManager.SystemState.OutputConsoleFontSize;
            Loaded += (s, e) => {
                //Output.IsEnabled = false;
                ConsoleIO.SetOutputConsole(this);
            };
        }

        private void Output_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                Output.FontSize += Math.Sign(e.Delta);
                if (Output.FontSize > 34) Output.FontSize = 34;
                if (Output.FontSize < 8) Output.FontSize = 8;
                StateManager.SystemState.OutputConsoleFontSize = Output.FontSize;
                e.Handled = true;
                
            }
        }

        public void WriteOutput(string text, Color color)
        {
            var output = $"{text}\n";
            Output.WriteOutput(output, color);
        }

        public void WriteOutput(string text)
        {
            WriteOutput(text, Colors.White);
        }

        public void WriteOutput(string format, params object[] args)
        {
            WriteOutput(string.Format(format, args));
        }

        public void WriteOutput(string format, Color color, params object[] args)
        {
            WriteOutput(string.Format(format, args), color);
        }

    }
}
