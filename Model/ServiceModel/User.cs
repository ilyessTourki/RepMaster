using System;
using SQLite;
using TrainSheet.Interface;

namespace TrainSheet.Model.ServiceModel
{
    public class User : IPrimaryKey
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }

}

