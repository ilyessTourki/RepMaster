using TrainSheet.Service;
using TrainSheet.Utilities;
using TrainSheet.ViewModel;

namespace TrainSheet.Control.WatchControl;

public partial class TimePicker : StackLayout
{
    private TimerBoxViewModel vm = ServiceHelper.GetService<TimerBoxViewModel>();

    public TimePicker()
	{
		InitializeComponent();
        BindingContext = vm;
	}
    private void OnHoursTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!int.TryParse(e.NewTextValue, out int value) || e.NewTextValue.Length > 2)
        {
            HoursEntry.Text = string.Empty;
        }
        else if (value > 9)
        {
            HoursEntry.Text = "01";
        }
    }
    private void OnMinutesTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!int.TryParse(e.NewTextValue, out int value) || e.NewTextValue.Length >2)
        {
            MinutesEntry.Text = string.Empty;
        }
        else if (value > 59)
        {
            MinutesEntry.Text = "59";
        }

        
    }
}
