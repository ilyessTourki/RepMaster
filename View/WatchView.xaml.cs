using TrainSheet.Utilities;
using TrainSheet.ViewModel;

namespace TrainSheet.View;

public partial class WatchView : ContentView
{
    private TimerBoxViewModel vm = ServiceHelper.GetService<TimerBoxViewModel>();

    public WatchView()
	{
		InitializeComponent();
        vm.SetComponents(ProgressCircle,TimeLabel,TotalLabel);
    }
    
    
}
