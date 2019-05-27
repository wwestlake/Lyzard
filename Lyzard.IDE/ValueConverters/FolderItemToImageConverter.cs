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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Lyzard.FileSystem;
using Lyzard.IDE.ViewModels;

namespace Lyzard.IDE.ValueConverters
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class FolderItemToImageConverter : IValueConverter
    {
        public static readonly FolderItemToImageConverter Instance = new FolderItemToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var uri = new Uri($"pack://application:,,,/Resources/Images/{value}", UriKind.RelativeOrAbsolute);
                var bm = new BitmapImage(uri);
                return bm;
            } catch (IOException)
            {
                var uri = new Uri($"pack://application:,,,/Resources/Images/FileTypes/file.png", UriKind.RelativeOrAbsolute);
                var bm = new BitmapImage(uri);
                return bm;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
