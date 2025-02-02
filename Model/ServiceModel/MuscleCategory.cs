using System;
using System.Collections.ObjectModel;
using SQLite;
using TrainSheet.Interface;
using TrainSheet.Model.Enum;

namespace TrainSheet.Model.ServiceModel;

public class MuscleCategory : IPrimaryKey
{
    [PrimaryKey, AutoIncrement]
    public int ID {get;set;}
    public MuscleEnum muscleType {get;set;}
    public string name {get;set;}
    public string image {get;set;}
    public Repetition bestRepetition {get;set;}
    public int bestWeight {get;set;}
    public List<List<Repetition>> lastRepetition {get;set;}

}
