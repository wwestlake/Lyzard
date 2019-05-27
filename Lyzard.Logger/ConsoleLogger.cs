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
using Lyzard.Interfaces;
using Lyzard.SystemIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lyzard.Logger
{
    public class ConsoleLogger : SystemLog
    {
        private Dictionary<LogEntryType, Color> _colors = new Dictionary<LogEntryType, Color>()
        {
            { LogEntryType.Info, Colors.LightBlue },
            { LogEntryType.Warning, Colors.Yellow },
            { LogEntryType.Error, Colors.Salmon },
            { LogEntryType.Exception, Colors.PaleVioletRed },
            { LogEntryType.Critical, Colors.Red },
        };

        internal ConsoleLogger()
        {

        }

        public override void Log(LogEntryType type, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            ConsoleIO.WriteOutput(string.Format("{0}: {1}: {2}", type, DateTime.Now, msg), _colors[type]);
        }
    }
}
