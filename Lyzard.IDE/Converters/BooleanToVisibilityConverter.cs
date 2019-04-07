using System.Windows;

namespace Lyzard.IDE.Converters
{
    public sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanToVisibilityConverter() :
          base(Visibility.Visible, Visibility.Collapsed)
        { }
    }
}
