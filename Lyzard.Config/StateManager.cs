using Lyzard.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Config
{
    public static class StateManager
    {

        public static readonly string StateFile =
            CommonFolders.LyzardConfig + CommonFolders.Sep + "LyzardState.json";

        private static ManagedFile _stateFile;
        private static SystemState _stateInstance;

        public static SystemState SystemState
        {
            get
            {
                if (_stateInstance != null) return _stateInstance;
                _stateFile = new ManagedFile(StateFile);
                if (_stateFile.Load().Length == 0)
                {
                    _stateInstance = new SystemState();
                    _stateInstance.Changed += _stateInstance_Changed;
                    SaveState();
                }
                else
                {
                    LoadState();
                    _stateInstance.Changed += _stateInstance_Changed;
                }
                return _stateInstance;
            }
        }

        private static void _stateInstance_Changed(object sender, EventArgs e)
        {
            SaveState();
        }

        private static void LoadState()
        {
            _stateInstance = _stateFile.Load<SystemState>();
        }

        private static void SaveState()
        {
            _stateFile.Save(_stateInstance);
        }
    }
}
