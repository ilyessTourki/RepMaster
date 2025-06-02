using System.Collections.ObjectModel;
using System.Windows.Input;
using Android.Icu.Text;
using CommunityToolkit.Mvvm.Input;
using TrainSheet.Model.ServiceModel;

namespace TrainSheet.View;

public partial class ProfileView : ContentView
{
	public ObservableCollection<BodyParts> bodyParts { get; set; } = new ObservableCollection<BodyParts>();
    public ICommand editUserInfo { get; }
    public bool isEditingUser { get; set; }
    public string editUserIcon { get; set; }

    public ProfileView()
    {
        InitializeComponent();
        BindingContext = this;
        SetBodyParts();
        isEditingUser = false;
        editUserIcon = "edit";
        editUserInfo = new Command(EditUserInfo);
       
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
    private void EditUserInfo()
    {
        isEditingUser = !isEditingUser;
        OnPropertyChanged(nameof(isEditingUser));
        editUserIcon = isEditingUser? "check" : "edit";
        OnPropertyChanged(nameof(editUserIcon));
    }


}
