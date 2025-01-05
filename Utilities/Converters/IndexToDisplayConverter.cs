using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using TrainSheet.Model;
namespace TrainSheet.Utilities.Converters;

public class IndexToDisplayConverter : IValueConverter
{
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
       if (parameter is BindingProxy proxy)
    {
        Debug.WriteLine("Parameter is a BindingProxy");
        if (proxy.Data is MuscleCategory muscleCategory)
        {
            Debug.WriteLine($"Parameter Data: {muscleCategory}");
        }
        else
        {
            Debug.WriteLine($"Parameter Data is of type: {proxy.Data?.GetType().Name ?? "null"}");
        }
    }
    else
    {
        Debug.WriteLine("Parameter is not a BindingProxy");
    }
         return "0.0";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
