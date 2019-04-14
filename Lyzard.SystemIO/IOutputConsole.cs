using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lyzard.SystemIO
{
    public interface IOutputConsole
    {
        void WriteOutput(string text, Color color);

        void WriteOutput(string text);

        void WriteOutput(string format, params object[] args);

        void WriteOutput(string format, Color color, params object[] args);

    }
}
