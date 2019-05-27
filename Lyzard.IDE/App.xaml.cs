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
using Lyzard.IDE.Views;
using Lyzard.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Lyzard.IDE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SystemLog.Instance.LogInfo($"Starting Lyzard");
            SystemLog.Instance.LogWarning($"Warning log test");
            SystemLog.Instance.LogError($"Error log test");
            SystemLog.Instance.LogException($"Exception log test");
            SystemLog.Instance.LogCritical($"Critical log test");

            var splash = new LyzardSplash();
            this.MainWindow = splash;
            splash.Show();

            var rand = new Random((int)DateTime.Now.Ticks);

            Task.Factory.StartNew(() =>
            {
                var delay = rand.Next(50, 250);
                //we need to do the work in batches so that we can report progress
                for (int i = 1; i <= 100; i++)
                {
                    if (i % 5 == 0) delay = rand.Next(50, 150);

                    // Load plugins and other resources here
                    //System.Threading.Thread.Sleep(delay);

                    //because we're not on the UI thread, we need to use the Dispatcher
                    //associated with the splash screen to update the progress bar
                    splash.Dispatcher.Invoke(() => splash.Progress = i);
                }

                //once we're done we need to use the Dispatcher
                //to create and show the main window
                this.Dispatcher.Invoke(() =>
                {
                    //initialize the main window, set it as the application main window
                    //and close the splash screen

                    var mainWindow = new MainWindow();
                    MainWindow = mainWindow;
                    mainWindow.Show();

                    //var startWindow = new StartupWindow();
                    //startWindow.Show();

                    splash.Close();
                });
            });

        }
    }
}
