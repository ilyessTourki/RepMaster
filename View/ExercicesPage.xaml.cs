namespace TrainSheet.View;
using TrainSheet.Model;
using TrainSheet.Model.Enum;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TrainSheet.Utilities;
using TrainSheet.Model.ServiceModel;
using static TrainSheet.Utilities.Utilities;
using TrainSheet.ViewModel;

public partial class ExercicesPage : ContentView
{
	public List<MuscleCategory> musclceExercices {get;set;} = new List<MuscleCategory>();
    private MuscleEnum muscle;
    public string exerciceTitle { get; set; }
	public ICommand exerciceDetail { get; }
    private MuscleDetailsVM muscleDetailsVM = ServiceHelper.GetService<MuscleDetailsVM>();
    private ContentView previousView;

    public ExercicesPage(MuscleEnum muscleEx, ContentView previousV)
	{
		InitializeComponent();
		muscle = muscleEx;
        previousView = previousV;
        exerciceDetail = new AsyncRelayCommand<MuscleCategory>(GoToExerciceDetail);
		var horizontalLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical)
            {
                VerticalItemSpacing = 7,
                HorizontalItemSpacing = 4
            };
		MyCollectionView.ItemsLayout = horizontalLayout;
		BindingContext = this;
	}
    public async Task OnViewAppeard()
    {
        await SetMuscleExercices();
    }
    private async Task SetMuscleExercices()
	{
        musclceExercices = await exercicesDB.GetAllAsync();
        // Map muscle enums to display titles
        var muscleTitles = new Dictionary<MuscleEnum, string>
        {
            { MuscleEnum.Pec, "CHEST" },
            { MuscleEnum.Frontarms, "ForeArms" },
            { MuscleEnum.Back, "BACK" },
            { MuscleEnum.Shoulder, "SHOULDER" },
            { MuscleEnum.Bieceps, "BICEPS" },
            { MuscleEnum.Triceps, "TRICEPS" },
            { MuscleEnum.Legs, "LEGS" },
            { MuscleEnum.Calisthenics, "CALISTHENICS" }
        };

        // Filter exercises by selected muscle
        musclceExercices = musclceExercices
            .Where(e => e.muscleType == muscle)
            .OrderByDescending(e => e.lastUpdated)
            .ToList();

        // Set the title using the dictionary
        exerciceTitle = muscleTitles.ContainsKey(muscle) ? muscleTitles[muscle] : "UNKNOWN";

        OnPropertyChanged(nameof(musclceExercices));
        OnPropertyChanged(nameof(exerciceTitle));
    }
	private async Task GoToExerciceDetail(MuscleCategory muscleCateg)
    {
        var detailsPage = new DetailsPage(muscleCateg,this);
        muscleDetailsVM.SetCurrentView(detailsPage);
	}

    void BackButton_Clicked(System.Object sender, System.EventArgs e)
    {
        muscleDetailsVM.SetCurrentView(previousView);
    }
}