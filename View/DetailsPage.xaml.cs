namespace TrainSheet.View;
using TrainSheet.Model.ServiceModel;
using TrainSheet.Popup;
using TrainSheet.Utilities;
using TrainSheet.ViewModel;

public partial class DetailsPage : ContentPage
{
	
	private MuscleDetailsVM muscleDetailsVM = ServiceHelper.GetService<MuscleDetailsVM>();

    public DetailsPage(MuscleCategory muscleCateg)
	{
		InitializeComponent();
        BindingContext = muscleDetailsVM;
		muscleDetailsVM.SetMuscle(muscleCateg,Navigation);
    }


}