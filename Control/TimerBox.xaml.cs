using TrainSheet.Service;
using TrainSheet.Utilities;
using TrainSheet.ViewModel;

namespace TrainSheet.Control;

public partial class TimerBox : ContentView
{
    private TimerBoxViewModel vm = ServiceHelper.GetService<TimerBoxViewModel>();

    public TimerBox()
	{
        InitializeComponent();
        BindingContext = vm;
    }
}
