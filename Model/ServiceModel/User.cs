using System;
using SQLite;
using TrainSheet.Interface;

namespace TrainSheet.Model.ServiceModel
{
    public class User : IPrimaryKey
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Image { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int BMI { get; set; }
    }

}

