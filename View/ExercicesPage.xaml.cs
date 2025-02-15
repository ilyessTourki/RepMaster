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
	public ICommand exerciceDetail { get; }
    public ExercicesPage(MuscleEnum muscleEx)
	{
		InitializeComponent();
		muscle = muscleEx;
        exerciceDetail = new AsyncRelayCommand<MuscleCategory>(GoToExerciceDetail);
		var horizontalLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical)
            {
                VerticalItemSpacing = 4,
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
                break;
            case MuscleEnum.Frontarms:
                musclceExercices = Constants.FrontArmsExercices;
                break;
            case MuscleEnum.Back:
                musclceExercices = Constants.BackExercices;
                break;
            case MuscleEnum.Shoulder:
                musclceExercices = Constants.ShouldersExercices;
                break;
            case MuscleEnum.Bieceps:
                musclceExercices = Constants.BicepsExercices;
                break;
            case MuscleEnum.Triceps:
                musclceExercices = Constants.TricepsExercices;
                break;
            case MuscleEnum.Legs:
                musclceExercices = Constants.LegsExercices;
                break;
        }
        OnPropertyChanged(nameof(musclceExercices));
    }
	private async Task GoToExerciceDetail(MuscleCategory muscleCateg)
	{
		 await Navigation.PushAsync(new DetailsPage(muscleCateg));
	}
}