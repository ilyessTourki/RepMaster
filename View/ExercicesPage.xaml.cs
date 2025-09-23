namespace TrainSheet.View;
using TrainSheet.Model;
using TrainSheet.Model.Enum;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TrainSheet.Utilities;
using TrainSheet.Model.ServiceModel;
using static TrainSheet.Utilities.Utilities;

public partial class ExercicesPage : ContentPage
{
	public List<MuscleCategory> musclceExercices {get;set;} = new List<MuscleCategory>();
    private MuscleEnum muscle;
    public string exerciceTitle { get; set; }
	public ICommand exerciceDetail { get; }
    public ExercicesPage(MuscleEnum muscleEx)
	{
		InitializeComponent();
		muscle = muscleEx;
        exerciceDetail = new AsyncRelayCommand<MuscleCategory>(GoToExerciceDetail);
		var horizontalLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical)
            {
                VerticalItemSpacing = 7,
                HorizontalItemSpacing = 4
            };
		MyCollectionView.ItemsLayout = horizontalLayout;
		BindingContext = this;
	}
    protected async override void OnAppearing()
    {
        await SetMuscleExercices();
    }
    private async Task SetMuscleExercices()
	{
		switch (muscle)
		{
			case MuscleEnum.Pec:
                musclceExercices = await pecCategDB.GetAllAsync();
                exerciceTitle = "CHEST";
                break;
            case MuscleEnum.Frontarms:
                musclceExercices = Constants.FrontArmsExercices;
                exerciceTitle = "ForeArms";
                break;
            case MuscleEnum.Back:
                musclceExercices = Constants.BackExercices;
                exerciceTitle = "BACK";
                break;
            case MuscleEnum.Shoulder:
                musclceExercices = Constants.ShouldersExercices;
                exerciceTitle = "SHOULDER";
                break;
            case MuscleEnum.Bieceps:
                musclceExercices = Constants.BicepsExercices;
                exerciceTitle = "BIECEPS";
                break;
            case MuscleEnum.Triceps:
                musclceExercices = Constants.TricepsExercices;
                exerciceTitle = "TRICEPS";
                break;
            case MuscleEnum.Legs:
                musclceExercices = Constants.LegsExercices;
                exerciceTitle = "LEGS";
                break;
            case MuscleEnum.Calisthenics:
                musclceExercices = Constants.CalisthenicsExercices;
                exerciceTitle = "CALISTHENICS";
                break;
        }
        OnPropertyChanged(nameof(musclceExercices));
        OnPropertyChanged(nameof(exerciceTitle));
    }
	private async Task GoToExerciceDetail(MuscleCategory muscleCateg)
	{
		 await Navigation.PushAsync(new DetailsPage(muscleCateg));
	}
}