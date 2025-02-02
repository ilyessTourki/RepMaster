using System;
using SQLite;
using TrainSheet.Interface;

namespace TrainSheet.Model;

public class Repetition : IPrimaryKey
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int weight  {get;set;}
    public int repetion  {get;set;}
}
