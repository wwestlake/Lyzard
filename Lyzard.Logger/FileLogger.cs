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
using Lyzard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Lyzard.Logger
{
    public class FileLogger : SystemLog
    {

        private string _logPath;
        private ManagedFile _logFile;
        private DispatcherTimer _timer;

        internal FileLogger()
        {
            CreateLogFile();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(60);
            _timer.Tick += CreateLogFile;
            _timer.Start();
        }

        public override void Log(LogEntryType type, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            _logFile.Append(string.Format("{0}: {1}: {2}\n", type.ToString().ToUpper(), DateTime.Now, msg));
        }

        private string LogFileName()
        {
            return CommonFolders.LyzardLogs + CommonFolders.Sep + DateTime.Now.ToString("yyyyMMdd") + ".log";
        }

        private void CreateLogFile()
        {
            if (_logPath != LogFileName())
            {
                _logPath = LogFileName();
                _logFile = ManagedFile.Create(_logPath);
            }
        }

        private void CreateLogFile(object sender, EventArgs e)
        {
            CreateLogFile();
        }

    }
}
