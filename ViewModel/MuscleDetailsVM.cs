using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Java.Util;
using Mopups.Services;
using TrainSheet.Model;
using TrainSheet.Model.ServiceModel;
using TrainSheet.Popup;
using TrainSheet.Utilities;

namespace TrainSheet.ViewModel
{
	public class MuscleDetailsVM :BindableObject
	{
        public MuscleCategory machineTrain { get; set; } = new MuscleCategory();
        public ICommand exerciceEditor { get; }
        public ICommand addItem { get; }
        public ICommand deleteSet { get; }
        public ICommand backButton { get; }
        public ObservableCollection<int> setsNumber { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<List<Repetition>> sets { get; set; } = new ObservableCollection<List<Repetition>>();
        private int repetitionIndex = 0;
        private INavigation Navigation;


        public MuscleDetailsVM()
		{
            exerciceEditor  = new Command<ObservableCollection<Repetition>>(Edit_Clicke);
            deleteSet       = new Command<ObservableCollection<Repetition>>(DeleteSetClicked);
            addItem         = new Command(addItemToList);
            backButton      = new AsyncRelayCommand(navigateBackward);

        }
        public void SetMuscle(MuscleCategory muscleCateg,INavigation navigation)
        {
            Navigation = navigation;
            setExercicesSets(muscleCateg);
            SetSetNumber();
        }
        private void setExercicesSets(MuscleCategory muscleCateg)
        {
            machineTrain = muscleCateg;
            if(muscleCateg.lastRepetition != null && muscleCateg.lastRepetition.Count > 0)
            {
                foreach (var set in muscleCateg.lastRepetition)
                {
                    sets.Add(set);
                }
                OnPropertyChanged(nameof(sets));
            }
            OnPropertyChanged(nameof(machineTrain));
        }
        private void Edit_Clicke(ObservableCollection<Repetition> repetitions)
        {
            repetitionIndex = machineTrain.lastRepetition.ToList().FindIndex(rep => rep.Equals(repetitions));
            MopupService.Instance.PushAsync(new SeriesEditPopup(repetitions));
        }
        private void SetSetNumber()
        {
            if (machineTrain != null && machineTrain.lastRepetition != null)
            {
                for (int i = 1; i < machineTrain.lastRepetition.Count + 1; i++)
                {
                    setsNumber.Add(i);
                }
                OnPropertyChanged(nameof(setsNumber));
            }
        }
        private void addItemToList()
        {
            
            repetitionIndex = -1;
            var newrepetition = new ObservableCollection<Repetition>
            {
                new Repetition
                {
                    repetion=0,
                    weight = 0
                }
            };
            MopupService.Instance.PushAsync(new SeriesEditPopup(newrepetition));
        }
        private void DeleteSetClicked(ObservableCollection<Repetition> repetitions)
        {
            int setIndex = machineTrain.lastRepetition.ToList().FindIndex(rep => rep.Equals(repetitions));
            machineTrain.lastRepetition.RemoveAt(setIndex);
            OnPropertyChanged(nameof(machineTrain.lastRepetition));
            setsNumber.Clear();
            SetSetNumber();
            UpdateBestRepetition();
        }
        public void UpdateRepetitions(List<Repetition> selectedRepetition)
        {
            if (repetitionIndex >= 0 && sets[repetitionIndex] != null)
            {
                sets[repetitionIndex] = selectedRepetition;
            }
            else if(repetitionIndex == -1)
            {
                List<List<Repetition>> lastRepetitions = new List<List<Repetition>>();
                if(sets != null)
                {
                    lastRepetitions = sets.ToList();
                    sets.Clear();
                }
                else
                {
                    sets = new ObservableCollection<List<Repetition>>();
                }
                lastRepetitions.Add(selectedRepetition);
                
                foreach (var set in lastRepetitions)
                {
                    sets.Add(set);
                }
                setsNumber.Add(setsNumber.Count + 1);
                OnPropertyChanged(nameof(setsNumber));
            }
            
            SetBestRepetition(selectedRepetition);
            OnPropertyChanged(nameof(sets));
            Debug.WriteLine(sets);
        }
        private void SetBestRepetition(List<Repetition> selectedRepetition)
        {
            int bestWeight = 0;
            Repetition bestRepetition = new Repetition { weight=0,repetion=0};
            foreach (var set in selectedRepetition)
            {
                if (set.weight > bestWeight)
                    bestWeight = set.weight;
                if (set.repetion > bestRepetition.repetion)
                    bestRepetition = set;
            }
            if (bestWeight > machineTrain.bestWeight)
                machineTrain.bestWeight = bestWeight;
            if (machineTrain.bestRepetition== null || bestRepetition.repetion > machineTrain.bestRepetition.repetion)
                machineTrain.bestRepetition = bestRepetition;
            OnPropertyChanged(nameof(machineTrain));
        }
        private void UpdateBestRepetition()
        {
            int bestWeight = 0;
            Repetition bestRepetition = new Repetition { weight = 0, repetion = 0 };
            foreach (var set in machineTrain.lastRepetition)
            {
                foreach (var repet in set)
                {
                    if (repet.weight > bestWeight)
                        bestWeight = repet.weight;
                    if (repet.repetion > bestRepetition.repetion)
                        bestRepetition = repet;
                }
            }
            machineTrain.bestWeight = bestWeight;
            machineTrain.bestRepetition = bestRepetition;
            OnPropertyChanged(nameof(machineTrain));
        }
        private async Task navigateBackward()
        {
            await Navigation.PopAsync();
        }
    }
}

