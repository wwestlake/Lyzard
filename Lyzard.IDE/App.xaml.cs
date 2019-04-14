using Lyzard.IDE.Views;
using System;
using System.Threading;
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
                    System.Threading.Thread.Sleep(delay);

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
                    splash.Close();
                });
            });

        }
    }
}
