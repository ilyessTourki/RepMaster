
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TrainSheet.Model;
using TrainSheet.Model.Enum;

namespace TrainSheet.View;

public partial class MuscleType : ContentPage
{
	public List<Muscle>  muscles {get;set;}= new List<Muscle>();
	public ICommand muscleExercices { get; }


    public MuscleType()
    {
        InitializeComponent();
        muscles = new List<Muscle> {
            new Muscle{ muscleEnum= MuscleEnum.Pec,     image ="pec.png" },
            new Muscle{ muscleEnum= MuscleEnum.Back,    image ="back.png" },
            new Muscle{ muscleEnum= MuscleEnum.Shoulder,image ="shoulder.png" },
            new Muscle{ muscleEnum= MuscleEnum.Bieceps, image ="biceps.png" },
            new Muscle{ muscleEnum= MuscleEnum.Triceps, image ="triceps.png" },
            new Muscle{ muscleEnum= MuscleEnum.Frontarms,image ="frontarms.png" },
            new Muscle{ muscleEnum= MuscleEnum.Legs,    image ="legs.png" },
            new Muscle{ muscleEnum= MuscleEnum.Abs,     image ="abs.png" }};
        muscleExercices = new AsyncRelayCommand<MuscleEnum>(GoToMuscleExercices);
        BindingContext = this;
		 var horizontalLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical)
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
}