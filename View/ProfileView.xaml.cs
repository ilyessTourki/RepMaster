using System.Collections.ObjectModel;
using System.Windows.Input;
using Android.Icu.Text;
using CommunityToolkit.Mvvm.Input;
using TrainSheet.Model.ServiceModel;

namespace TrainSheet.View;

public partial class ProfileView : ContentView
{
	public ObservableCollection<BodyParts> bodyParts { get; set; } = new ObservableCollection<BodyParts>();
    public bool     isEditingMesurments { get; set; }
    public string   editUserMesurments { get; set; }
    public ICommand editUserMesurment { get; }

    public ProfileView()
    {
        InitializeComponent();
        isEditingMesurments = false;
        editUserMesurments = "edit";
        editUserMesurment = new Command(EditUserMesurment);
        BindingContext = this;
        SetBodyParts();
    }
	private void SetBodyParts()
	{
        bodyParts = new ObservableCollection<BodyParts> {
            new BodyParts { Name = "Neck", Icon = "neck", Mesure = "58 cm" },
           new BodyParts { Name = "Shoulder", Icon = "back", Mesure = "146 cm" },
        new BodyParts { Name = "Chest", Icon = "chest", Mesure = "106 cm" },
        new BodyParts { Name = "Right Biceps", Icon = "biceps", Mesure = "42 cm" },
        new BodyParts { Name = "Left Biceps", Icon = "biceps", Mesure = "40 cm" },
        new BodyParts { Name = "Right Forearm", Icon = "forearm", Mesure = "32 cm" },
        new BodyParts { Name = "Left Forearm", Icon = "forearm", Mesure = "32 cm" },
        new BodyParts { Name = "Waist", Icon = "waist", Mesure = "186 cm" },
        new BodyParts { Name = "Left Calves", Icon = "calves", Mesure = "76 cm" },
        new BodyParts { Name = "Right Calves", Icon = "calves", Mesure = "76 cm" },
        new BodyParts { Name = "Left Thighs", Icon = "thighs", Mesure = "106 cm" },
        new BodyParts { Name = "Right Thighs", Icon = "thighs", Mesure = "106 cm" }
    };

        OnPropertyChanged(nameof(bodyParts));
        bodyPartsListView.ItemsSource = bodyParts;

    }
    private void EditUserMesurment()
    {
        isEditingMesurments = !isEditingMesurments;
        OnPropertyChanged(nameof(isEditingMesurments));
        editUserMesurments = isEditingMesurments ? "check" : "edit";
        OnPropertyChanged(nameof(editUserMesurments));
    }


}
