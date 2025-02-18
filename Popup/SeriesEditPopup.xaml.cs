namespace TrainSheet.Popup;

using System.Collections.ObjectModel;
using System.Windows.Input;
using TrainSheet.Model;
using TrainSheet.Utilities;
using TrainSheet.ViewModel;
using Mopups.Services;
using System.Diagnostics;
using CommunityToolkit;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

public partial class SeriesEditPopup 
{
    private MuscleDetailsVM muscleDetailsVM = ServiceHelper.GetService<MuscleDetailsVM>();
    public ObservableCollection<Repetition> selectedRepetition {get;set;}= new ObservableCollection<Repetition>();
    public ICommand addItem { get; set; }
    public ICommand deletItem {get;set;}
	public SeriesEditPopup(ObservableCollection<Repetition> repetitions)
	{
		
		InitializeComponent();
		foreach (var item in repetitions)
		{
			selectedRepetition.Add(item);
		}
		addItem 	= new Command(addItemToList);
		deletItem 	= new Command<Repetition>(deleteItemFromList);
		BindingContext = this;
	}
	private void addItemToList()
	{
		selectedRepetition.Add(new Repetition());
		OnPropertyChanged(nameof(selectedRepetition));
	}
	private void deleteItemFromList(Repetition repetition)
	{
		if (selectedRepetition.Contains(repetition))
        {
            selectedRepetition.Remove(repetition);
        }
		OnPropertyChanged(nameof(selectedRepetition));
	}

    async void Confirm_Clicked(System.Object sender, System.EventArgs e)
    {
		Debug.WriteLine(selectedRepetition);
		bool isNullRepetetion = false;
		foreach (var rep in selectedRepetition)
		{
			if(rep.repetion == 0 || rep.weight == 0)
			{
				isNullRepetetion = true;
            }
		}
		if (!isNullRepetetion)
		{
            muscleDetailsVM.UpdateRepetitions(selectedRepetition.ToList());
            await MopupService.Instance.PopAllAsync();
		}
		else
		{
            var toast = Toast.Make("One or more entries are still 0 !", ToastDuration.Long);
            await toast.Show();
        }
    }

    async void Cancel_Clicked(System.Object sender, System.EventArgs e)
    {
		await MopupService.Instance.PopAllAsync();
    }
}