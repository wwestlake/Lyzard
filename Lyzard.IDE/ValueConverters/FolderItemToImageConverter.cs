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
