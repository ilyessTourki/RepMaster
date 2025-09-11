using System.Windows.Input;
using Microsoft.Maui.Controls;
using TrainSheet.Utilities;
using TrainSheet.ViewModel;

namespace TrainSheet.Control.ProfileControl;

public partial class GeneralInfoControl : StackLayout
{
    private ProfileVM profileVM = ServiceHelper.GetService<ProfileVM>();

    public GeneralInfoControl()
	{
		InitializeComponent();
        BindingContext = profileVM;
        var horizontalLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Horizontal)
        {
            VerticalItemSpacing = 10,
            HorizontalItemSpacing = 4
        };

        userInfoCollection.ItemsLayout = horizontalLayout;
    }
   
}
