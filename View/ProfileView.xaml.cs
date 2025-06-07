using System.Collections.ObjectModel;
using System.Windows.Input;
using static TrainSheet.Utilities.Utilities;
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
    public async Task OnViewAppeard()
    {
        await SetBodyParts();
    }
    private async Task SetBodyParts()
	{
        if(bodyParts.Count == 0)
        {
            var bodyPartsFromDB = await bodyPartsDB.GetAllAsync();
            foreach (var bodyPart in bodyPartsFromDB)
            {
                bodyParts.Add(bodyPart);
            }
            OnPropertyChanged(nameof(bodyParts));
            bodyPartsListView.ItemsSource = bodyParts;
        }

    }
    private void EditUserMesurment()
    {
        isEditingMesurments = !isEditingMesurments;
        OnPropertyChanged(nameof(isEditingMesurments));
        editUserMesurments = isEditingMesurments ? "check" : "edit";
        OnPropertyChanged(nameof(editUserMesurments));
    }


}
