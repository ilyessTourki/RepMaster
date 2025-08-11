using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TrainSheet.Model;
using TrainSheet.Model.Enum;
using TrainSheet.Model.ServiceModel;
using static TrainSheet.Utilities.Utilities;
using static TrainSheet.Utilities.Constants;

namespace TrainSheet.View;

public partial class BodyPartsView : ContentView
{
    public List<Muscle> muscles { get; set; } = new List<Muscle>();
    public ICommand muscleExercices { get; }

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
        new Muscle{ name = "CALISTHENICS"        ,muscleEnum= MuscleEnum.Calisthenics,     image ="abs.png" }};
        muscleExercices = new AsyncRelayCommand<MuscleEnum>(GoToMuscleExercices);
        BindingContext = this;
        var horizontalLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical)
        {
            VerticalItemSpacing = 4,
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
}
