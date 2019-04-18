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
