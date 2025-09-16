using System.ComponentModel;
using SQLite;
using TrainSheet.Interface;

namespace TrainSheet.Model.ServiceModel;

[Preserve(AllMembers = true)]

public class BodyParts : INotifyPropertyChanged, IPrimaryKey
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    public string Name { get; set; }

    public string Icon { get; set; }

    private double mesure;

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public double Mesure {
        get => mesure ; set {
            if (mesure != value)
            {
                mesure = value;
                OnPropertyChanged(nameof(Mesure));
            }
        } }
}
