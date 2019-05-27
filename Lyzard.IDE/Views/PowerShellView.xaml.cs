/* 
 * Lyzard Modeling and Simulation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
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
