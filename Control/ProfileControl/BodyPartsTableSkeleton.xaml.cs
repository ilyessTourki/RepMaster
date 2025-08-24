using System.Collections.ObjectModel;

namespace TrainSheet.Control.ProfileControl;

public partial class BodyPartsTableSkeleton : ListView
{
	public ObservableCollection<int> skeletonList { get; set; } = new ObservableCollection<int>() { 1,2,3,4,5,6,7,8,9,10};
	public bool isVisible { get; set; }

    public BodyPartsTableSkeleton()
	{
		InitializeComponent();
        BindingContext = this;
    }
	public void SetVisibleanimation()
	{
        isVisible = true;
        OnPropertyChanged(nameof(isVisible));
    }
}
