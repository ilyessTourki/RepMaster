using System;
using System.Collections.ObjectModel;
using TrainSheet.Model.Enum;

namespace TrainSheet.Model;

public class MuscleCategory : BindableObject
{
    public int ID {get;set;}
    public MuscleEnum muscleType {get;set;}
    public string name {get;set;}
    public string image {get;set;}
    public Repetition bestRepetition {get;set;}
    public int bestWeight {get;set;}
    public ObservableCollection<ObservableCollection<Repetition>> lastRepetition {get;set;}

}
