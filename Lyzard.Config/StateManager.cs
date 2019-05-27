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
