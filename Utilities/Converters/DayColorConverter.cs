using System;
using System.Globalization;
using TrainSheet.Model;

namespace TrainSheet.Utilities.Converters
{
	public class DayColorConverter : IValueConverter
    {
        public Color SelectedColor { get; set; } 
        public Color DefaultColor { get; set; }
        public Color HasSetsColor { get; set; } 

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (App.Current.Resources.TryGetValue("PrimaryDark", out var PrimaryDark))
                SelectedColor = (Color)PrimaryDark;
            if (App.Current.Resources.TryGetValue("DarkGray", out var DarkGray))
                DefaultColor = (Color)DarkGray;
            if (App.Current.Resources.TryGetValue("PrimaryLight", out var PrimaryLight))
                HasSetsColor = (Color)PrimaryLight;

            if (value is DayItem day)
            {
                if (day.IsSelected)
                    return SelectedColor;
                if (day.HasSets)
                    return HasSetsColor;
                return DefaultColor;
            }
            return DefaultColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

