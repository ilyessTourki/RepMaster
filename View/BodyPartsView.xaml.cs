using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TrainSheet.Model;
using TrainSheet.Model.Enum;
using TrainSheet.Model.ServiceModel;
using static TrainSheet.Utilities.Utilities;
using static TrainSheet.Utilities.Constants;
using System.Collections.ObjectModel;

namespace TrainSheet.View;

public partial class BodyPartsView : ContentView
{
    public List<Muscle> muscles { get; set; } = new List<Muscle>();
    public ICommand muscleExercices { get; }
    public ObservableCollection<DayItem> WeekDays { get; set; }


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
        SetDaysCollection();
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
        await Navigation.PushAsync(new ExercicesPage(muscle));
    }
	public async Task OnViewAppeard()
	{
        await SavePecExercices(pecCategDB);
    }
    private async Task SavePecExercices(Service.SQLiteDataAccess<MuscleCategory> muscleCateg)
    {
        muscleCateg.InitializeAsync(SQLiteDataAccessPath);
        var listPecExercices = await muscleCateg.GetAllAsync();
        if (!listPecExercices.Any())
        {
            foreach (var pecExo in PecExercices)
            {
                await muscleCateg.SaveAsync(pecExo);
            }
        }
        else if (listPecExercices.Count != PecExercices.Count)
        {
            foreach (var pecExo in PecExercices)
            {
                if (!listPecExercices.Any(b => b.name == pecExo.name))
                {
                    await muscleCateg.SaveAsync(pecExo);
                }
            }
        }
    }
    private void SetDaysCollection()
    {
        var today = DateTime.Today;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek);

        WeekDays = new ObservableCollection<DayItem>();

        for (int i = 0; i < 7; i++)
        {
            var date = startOfWeek.AddDays(i);
            WeekDays.Add(new DayItem
            {
                DayName = date.ToString("ddd"),
                DayNumber = date.Day,
                Date = date,
                IsSelected = date == today,
                HasSets = (date < today && date.Day % 2 == 0) // 👈 Example: fake rule (even days = has sets)
            });
        }
    }
}
