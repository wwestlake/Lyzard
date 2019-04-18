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
        private static Queue<Tuple<string, Color>> _cache = new Queue<Tuple<string, Color>>();

        static ConsoleIO()
        {
        }

        private static IOutputConsole Output;

        public static void SetOutputConsole(IOutputConsole output)
        {
            Output = output;
            OuputCache();
        }


        public static void WriteOutput(string text, Color color)
        {
            if (Output == null)
            {
                Cache(text, color);
                return;
            }
            Output.WriteOutput(text, color);
        }


        public static void WriteOutput(string text)
        {
            WriteOutput(text, Colors.White);
        }

        public static void WriteOutput(string format, params object[] args)
        {
            WriteOutput(string.Format(format, args));
        }

        public static void WriteOutput(string format, Color color, params object[] args)
        {
            WriteOutput(string.Format(format, args), color);
        }

        private static void Cache(string text, Color color)
        {
            _cache.Enqueue(Tuple.Create(text, color));
        }

        private static void OuputCache()
        {
            while (_cache.Count > 0)
            {
                var info = _cache.Dequeue();
                WriteOutput(info.Item1, info.Item2);
            }
        }


    }
}
