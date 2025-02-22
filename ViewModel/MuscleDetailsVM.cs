using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using AndroidX.Lifecycle;
using CommunityToolkit.Mvvm.Input;
using Java.Util;
using Mopups.Services;
using TrainSheet.Model;
using TrainSheet.Model.ServiceModel;
using TrainSheet.Popup;
using static TrainSheet.Utilities.Utilities;

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
            exerciceEditor  = new Command<List<Repetition>>(Edit_Clicke);
            deleteSet       = new AsyncRelayCommand<List<Repetition>>(DeleteSetClicked);
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
        private void Edit_Clicke(List<Repetition> repetitions)
        {
            repetitionIndex = sets.ToList().FindIndex(rep => rep.Equals(repetitions));
            var exoRepetitions = new ObservableCollection<Repetition>();
            foreach (var rep in repetitions)
            {
                exoRepetitions.Add(rep);
            }
            MopupService.Instance.PushAsync(new SeriesEditPopup(exoRepetitions));
        }
        private void SetSetNumber()
        {
            if (sets != null)
            {
                for (int i = 1; i < sets.Count + 1; i++)
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
        private async Task DeleteSetClicked(List<Repetition> repetitions)
        {
            int setIndex = sets.ToList().FindIndex(rep => rep.Equals(repetitions));
            sets.RemoveAt(setIndex);
            OnPropertyChanged(nameof(sets));
            setsNumber.Clear();
            SetSetNumber();
            UpdateBestRepetition();
            await SaveExerciceReps();
        }
        public async Task UpdateRepetitions(List<Repetition> selectedRepetition)
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

            UpdateBestRepetition();
            OnPropertyChanged(nameof(sets));

            await SaveExerciceReps();
        }
        private void UpdateBestRepetition()
        {
            int bestWeight = 0;
            Repetition bestRepetition = new Repetition { weight = 0, repetion = 0 };
            foreach (var set in sets)
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
        private async Task SaveExerciceReps()
        {
            if(sets != null && sets.Count > 0)
            {
                machineTrain.lastRepetition = new List<List<Repetition>>(sets);
            }
            await pecCategDB.SaveAsync(machineTrain);
            
        }
    }
}

