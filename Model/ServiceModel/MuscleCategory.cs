using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
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
    public int bestWeight {get;set;}
    [Ignore] // Prevents SQLite from treating this as a column
    public Repetition bestRepetition { get; set; }

    [Ignore]
    public List<List<Repetition>> lastRepetition { get; set; }

    // Store JSON representation in SQLite
    public string BestRepetitionJson
    {
        get => JsonConvert.SerializeObject(bestRepetition);
        set => bestRepetition = string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject<Repetition>(value);
    }

    public string LastRepetitionJson
    {
        get => JsonConvert.SerializeObject(lastRepetition);
        set => lastRepetition = string.IsNullOrEmpty(value) ? new List<List<Repetition>>() : JsonConvert.DeserializeObject<List<List<Repetition>>>(value);
    }

}
