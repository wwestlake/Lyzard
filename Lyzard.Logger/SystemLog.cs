using Lyzard.Config;
using Lyzard.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Interfaces
{
    public class SystemLog : ILogger
    {
        private static List<SystemLog> _loggers = new List<SystemLog>();
        private static SystemLog _instance;

        protected SystemLog()
        {
        }

        public static ILogger Instance
        {
            get
            {
                if (_loggers.Count == 0)
                {
                    _loggers.Add(new ConsoleLogger());
                    _loggers.Add(new FileLogger());
                }
                return _instance ?? (_instance = new SystemLog());
            }
        }


        public virtual void Log(LogEntryType type, string message, params object[] args)
        {
            if (type < SettingsManager.Instance.Settings.LogLevel) return;
            foreach (var logger in _loggers)
            {
                logger.Log(type, message, args);
            }
        }


        public void LogCritical(string message, params object[] args)
        {
            Log(LogEntryType.Critical, string.Format(message, args));
        }

        public void LogError(string message, params object[] args)
        {
            Log(LogEntryType.Error, string.Format(message, args));
        }

        public void LogException(string message, params object[] args)
        {
            Log(LogEntryType.Exception, string.Format(message, args));
        }

        public void LogInfo(string message, params object[] args)
        {
            Log(LogEntryType.Info, string.Format(message, args));
        }

        public void LogWarning(string message, params object[] args)
        {
            Log(LogEntryType.Warning, string.Format(message, args));
        }

         

    }
}
