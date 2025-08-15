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
        // Keep only digits
        if (!int.TryParse(e.NewTextValue, out int value))
        {
            HoursEntry.Text = string.Empty;
        }
    }
    private void OnMinutesTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!int.TryParse(e.NewTextValue, out int value))
        {
            MinutesEntry.Text = string.Empty;
        }
        else if (value > 59)
        {
            MinutesEntry.Text = "59";
        }

        
    }
}
