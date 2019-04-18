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
