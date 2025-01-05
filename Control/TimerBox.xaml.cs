using TrainSheet.Service;
using TrainSheet.Utilities;
using TrainSheet.ViewModel;

namespace TrainSheet.Control;

public partial class TimerBox : ContentView
{
	public TimerBox()
	{
        InitializeComponent();
        var timerService = ServiceHelper.GetService<TimerService>();
        BindingContext = new TimerBoxViewModel(timerService);
    }
}
