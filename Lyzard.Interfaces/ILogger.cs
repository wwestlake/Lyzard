using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lyzard.Interfaces
{
    public enum LogEntryType { Info, Warning, Error, Exception, Critical }

    public interface ILogger
    {
        void Log(LogEntryType type, string message, params object[] args);
        void LogInfo(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(string message, params object[] args);
        void LogException(string message, params object[] args);
        void LogCritical(string message, params object[] args);
    }
}
