using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Utilities
{
    public static class StringHelpers
    {
        public static string LeftOf(this string src, char chr)
        {
            return src.Substring(0, src.IndexOf(chr));
        }
    }
    
    public static class Helpers
    {
        public static void PrintLoadedAssemblies()
        {
            Assembly[] assys = AppDomain.CurrentDomain.GetAssemblies();
            Console.WriteLine("----------------------------------");
    
            foreach (Assembly assy in assys)
            {
                Console.WriteLine(assy.FullName.LeftOf(','));
            }
    
            Console.WriteLine("----------------------------------");
        }
    }
}
