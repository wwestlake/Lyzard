using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lyzard.IDE.ValueConverters
{
    /// <summary>
    /// Converts strings to double and back
    /// </summary>
    public class StringToDoubleConverter : IValueConverter
    {
        /// <summary>
        /// Converts a string to a double
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="targetType">the target type</param>
        /// <param name="parameter">the parameter</param>
        /// <param name="culture">the culture</param>
        /// <returns>0.0 if conversion fails</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result;
            var test = double.TryParse(value.ToString(), out result);
            if (test)
            {
                return result;
            }
            return 0.0;
        }

        /// <summary>
        /// Converts a double to a string
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <param name="targetType">the target type</param>
        /// <param name="parameter">the parameter</param>
        /// <param name="culture">the culture</param>
        /// <returns>The string</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }
    }
}
