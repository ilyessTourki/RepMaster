namespace TrainSheet.Popup;

using System.Collections.ObjectModel;
using System.Windows.Input;
using TrainSheet.Model;
using TrainSheet.Utilities;
using TrainSheet.ViewModel;
using Mopups.Services;

public partial class SeriesEditPopup 
{
    private MuscleDetailsVM muscleDetailsVM = ServiceHelper.GetService<MuscleDetailsVM>();
    public List<Repetition> selectedRepetition {get;set;}= new List<Repetition>();
	public ICommand addItem {get;set;}
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
        muscleDetailsVM.UpdateRepetitions(selectedRepetition);
        await MopupService.Instance.PopAllAsync();
    }

    async void Cancel_Clicked(System.Object sender, System.EventArgs e)
    {
		await MopupService.Instance.PopAllAsync();
    }
}