/* 
 * Lyzard Modeling and Simulation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
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
