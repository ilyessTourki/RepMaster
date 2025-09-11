using TrainSheet.Utilities;
using TrainSheet.ViewModel;

namespace TrainSheet.View;

public partial class ProfileView : ContentView
{

    private ProfileVM profileVM = ServiceHelper.GetService<ProfileVM>();

    public ProfileView()
    {
        InitializeComponent();
        BindingContext = profileVM;
    }
    public async Task OnViewAppeard()
    {
        profileVM.SetLoading(true);
        await profileVM.GetBodyParts();
        skeletonList.SetVisibleanimation();
        profileVM.SetUserPhoto();
        profileVM.SetUserInfos();
        profileVM.SetBodyParts();
        profileVM.SetLoading(false);
    }
   


}
