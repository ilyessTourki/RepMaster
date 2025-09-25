namespace TrainSheet.View;
using TrainSheet.Model.ServiceModel;
using TrainSheet.Popup;
using TrainSheet.Utilities;
using TrainSheet.ViewModel;

public partial class DetailsPage : ContentView
{
	
	private MuscleDetailsVM muscleDetailsVM = ServiceHelper.GetService<MuscleDetailsVM>();
    private ContentView previousView;

    public DetailsPage(MuscleCategory muscleCateg,ContentView previousV)
	{
		InitializeComponent();
        BindingContext = muscleDetailsVM;
        previousView = previousV;
		muscleDetailsVM.SetMuscle(muscleCateg,Navigation);
    }

    void BackButton_Clicked(System.Object sender, System.EventArgs e)
    {
        muscleDetailsVM.SetCurrentView(previousView);
    }

}