using SQLite;
using TrainSheet.Interface;

namespace TrainSheet.Model.ServiceModel;

public class BodyParts : IPrimaryKey
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    public string Name { get; set; }

    public string Icon { get; set; }

    public int Mesure { get; set; }
}
