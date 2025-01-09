namespace TrainSheet.View;

using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Input;
using Mopups.Services;
using TrainSheet.Model;
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