using System;
using System.Globalization;
using TrainSheet.Model.Enum;

namespace TrainSheet.Utilities.Converters
{
    public class MeasurementUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string measurement)
            {
                return measurement switch
                {
                    "WEIGHT" => " Kg",
                    "HEIGHT" => " cm",
                    "BMI" => string.Empty,
                    _ => string.Empty
                };
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

