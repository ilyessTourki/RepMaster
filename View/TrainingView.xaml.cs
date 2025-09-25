using TrainSheet.Utilities;
using TrainSheet.ViewModel;

namespace TrainSheet.View;

public partial class TrainingView : ContentView
{

    private MuscleDetailsVM muscleDetailsVM = ServiceHelper.GetService<MuscleDetailsVM>();
    public TrainingView()
	{
		InitializeComponent();
    }
    public async Task OnViewAppeard()
    {
        BindingContext = muscleDetailsVM;
        var bodyPartsView = new BodyPartsView();
        muscleDetailsVM.SetCurrentView(bodyPartsView);
        await bodyPartsView.OnViewAppeard();
    }
}
