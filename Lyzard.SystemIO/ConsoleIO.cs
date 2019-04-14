using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lyzard.SystemIO
{
    public static class ConsoleIO
    {
        static ConsoleIO()
        {
        }

        private static IOutputConsole Output;

        public static void SetOutputConsole(IOutputConsole output)
        {
            Output = output;
        }

        public static void WriteOutput(string text, Color color)
        {
            if (Output == null) return;
            Output.WriteOutput(text, color);
        }

        public static void WriteOutput(string text)
        {
            if (Output == null) return;
            Output.WriteOutput(text, Colors.White);
        }

        public static void WriteOutput(string format, params object[] args)
        {
            if (Output == null) return;
            WriteOutput(string.Format(format, args));
        }

        public static void WriteOutput(string format, Color color, params object[] args)
        {
            if (Output == null) return;
            WriteOutput(string.Format(format, args), color);
        }




    }
}
