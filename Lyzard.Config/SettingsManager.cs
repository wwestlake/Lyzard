using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using Lyzard.FileSystem;
using Lyzard.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lyzard.Config
{
    public sealed class SettingsManager
    {
        private static string _settingsPath = CommonFolders.LyzardConfig + CommonFolders.Sep + "Settings.json";
        private static ManagedFile _file;
        private static SettingsManager _instance;
        private DispatcherTimer _timer = new DispatcherTimer();

        private SettingsManager()
        {
            Settings = LoadSettings();
            _timer.Interval = TimeSpan.FromMinutes(5);
            _timer.Tick += SaveSettings;
        }

        public Settings Settings { get; private set; }

        public static SettingsManager Instance
        {
            get
            {
                return _instance ?? ( _instance = new SettingsManager());
            }
        }

        private Settings LoadSettings()
        {
            _file = ManagedFile.Create(_settingsPath);
            if (_file.Load().Length > 0)
            {
                return _file.Load<Settings>();
            } else
            {
                var settings = new Settings();
                _file.Save<Settings>(Settings);
                return settings;
            }
        }

        public void SaveSettings()
        {
            _file.Save<Settings>(Settings);
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            SaveSettings();
        }


 



        

    }
}
