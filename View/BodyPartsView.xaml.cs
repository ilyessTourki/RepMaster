using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TrainSheet.Model;
using TrainSheet.Model.Enum;
using TrainSheet.Model.ServiceModel;
using static TrainSheet.Utilities.Utilities;
using static TrainSheet.Utilities.Constants;
using System.Collections.ObjectModel;
using TrainSheet.ViewModel;
using TrainSheet.Utilities;

namespace TrainSheet.View;

public partial class BodyPartsView : ContentView
{
    public List<Muscle> muscles { get; set; } = new List<Muscle>();
    public ICommand muscleExercices { get; }
    public ObservableCollection<DayItem> WeekDays { get; set; }
    public List<MuscleCategory> listExercices { get; set; }
    private MuscleDetailsVM muscleDetailsVM = ServiceHelper.GetService<MuscleDetailsVM>();

    public BodyPartsView()
	{
		InitializeComponent();
        muscles = new List<Muscle> {
            new Muscle{ name = "CHEST"      ,muscleEnum= MuscleEnum.Pec,     image ="pec.png" },
            new Muscle{ name = "BACK"       ,muscleEnum= MuscleEnum.Back,    image ="back.png" },
            new Muscle{ name = "SHOULDER"   ,muscleEnum= MuscleEnum.Shoulder,image ="shoulder.png" },
            new Muscle{ name = "BICEPS"     ,muscleEnum= MuscleEnum.Bieceps, image ="biceps.png" },
            new Muscle{ name = "TRICEPS"    ,muscleEnum= MuscleEnum.Triceps, image ="triceps.png" },
            new Muscle{ name = "FOREARM"    ,muscleEnum= MuscleEnum.Frontarms,image ="frontarms.png" },
            new Muscle{ name = "LEGS"       ,muscleEnum= MuscleEnum.Legs,    image ="legs.png" },
            new Muscle{ name = "ABS"        ,muscleEnum= MuscleEnum.Abs,     image ="abs.png" },
            new Muscle{ name = "CALISTHENICS"        ,muscleEnum= MuscleEnum.Calisthenics,     image ="calisthenics.jpg" }};
        muscleExercices = new AsyncRelayCommand<MuscleEnum>(GoToMuscleExercices);
        BindingContext = this;
        var horizontalLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical)
        {
            VerticalItemSpacing = 10,
            HorizontalItemSpacing = 4
        };

        MyCollectionView.ItemsLayout = horizontalLayout;
    }
    private async Task GoToMuscleExercices(MuscleEnum muscle)
    {
        var exercicePage = new ExercicesPage(muscle , this);
        muscleDetailsVM.SetCurrentView(exercicePage);
        await exercicePage.OnViewAppeard();
    }
	public async Task OnViewAppeard()
	{
        await SavePecExercices();
        SetDaysCollection();
    }
    private async Task SavePecExercices()
    {
        exercicesDB.InitializeAsync(SQLiteDataAccessPath);
        listExercices = await exercicesDB.GetAllAsync();
        var allExercises = new List<MuscleCategory>();
        allExercises.AddRange(PecExercices);
        allExercises.AddRange(BackExercices);
        allExercises.AddRange(FrontArmsExercices);
        allExercises.AddRange(BicepsExercices);
        allExercises.AddRange(LegsExercices);
        allExercises.AddRange(ShouldersExercices);
        allExercises.AddRange(TricepsExercices);
        allExercises.AddRange(CalisthenicsExercices);

        if (!listExercices.Any())
        {
            foreach (var exo in allExercises)
            {
                await exercicesDB.SaveAsync(exo);
            }
        }
    }
    private void SetDaysCollection()
    {
        var today = DateTime.Today;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
        WeekDays = new ObservableCollection<DayItem>();

        // date only
        var updatedDates = listExercices
            .Select(e => e.lastUpdated.Date)
            .Distinct()
            .ToHashSet();

        for (int i = 0; i < 7; i++)
        {
            var date = startOfWeek.AddDays(i);
            WeekDays.Add(new DayItem
            {
                DayName = date.ToString("ddd"),
                DayNumber = date.Day,
                Date = date,
                IsSelected = date == today,
                HasSets = updatedDates.Contains(date)
            }) ;
        }
        OnPropertyChanged(nameof(WeekDays));
    }
}
