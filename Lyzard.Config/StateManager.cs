using Lyzard.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Lyzard.Config
{
    public static class StateManager
    {

        public static readonly string StateFile =
            CommonFolders.LyzardConfig + CommonFolders.Sep + "LyzardState.json";

        private static ManagedFile _stateFile;
        private static SystemState _stateInstance;

        static StateManager()
        {
            ScheduleSaveState();
        }

        public static SystemState SystemState
        {
            get
            {
                if (_stateInstance != null) return _stateInstance;
                _stateFile = ManagedFile.Create(StateFile);
                if (_stateFile.Load().Length == 0)
                {
                    _stateInstance = new SystemState();
                    SaveState();
                }
                else
                {
                    LoadState();
                }
                return _stateInstance;
            }
        }

        private static void ScheduleSaveState()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(20);
            timer.Tick += SaveState;
            timer.Start();
        }

        private static void LoadState()
        {
            _stateInstance = _stateFile.Load<SystemState>();
        }

        public static void SaveState()
        {
            _stateFile.Save(_stateInstance);
        }


        private static void SaveState(object sender, EventArgs e)
        {
            SaveState();
        }
    }
}
