using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lyzard.Utilities
{
    public static class RegexHelper
    {
        /// <summary>
        /// Checks a string for a match with a regex
        /// </summary>
        /// <param name="source">The source string</param>
        /// <param name="pattern">The pattern</param>
        /// <returns></returns>
        public static bool MatchRegex(this string source, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(source);
        }

        /// <summary>
        /// Test a string for a valid floating point number format
        /// </summary>
        /// <param name="source">The source string</param>
        /// <returns>True if valid number</returns>
        public static bool IsFloat(this string source)
        {
            return source.MatchRegex(@"[-+]?[0-9]*\.?[0-9]*([eE][-+]?[0-9]*)?");
        }

    }
}
