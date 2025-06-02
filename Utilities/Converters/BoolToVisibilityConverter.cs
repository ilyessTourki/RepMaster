using System;
using System.Globalization;

namespace TrainSheet.Utilities.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? false : true; // For IsVisible
                                                 // return boolValue ? Visibility.Visible : Visibility.Collapsed; (WPF-style)

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool visible)
                return visible;

            return false;
        }
    }
}

