using Plugin.LocalNotification;
using TrainSheet.Model.ServiceModel;
using TrainSheet.Utilities;
using TrainSheet.ViewModel;
using static TrainSheet.Utilities.Utilities;

namespace TrainSheet.View;

public partial class MainPage : ContentPage
{
    private ProfileVM profileVM = ServiceHelper.GetService<ProfileVM>();

    public MainPage()
	{
		InitializeComponent();
        BindingContext = this;
    }
    protected override async void OnAppearing()
    {
        TabSwitcher.SelectedIndex =0;
        await AskPermissions();
        await CreateUser();
        await CreateBodyPartsMesures();
    }
    private async Task AskPermissions()
    {
        if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
        {
            // Basic permission request
            await LocalNotificationCenter.Current.RequestNotificationPermission();
        }
    }
    async void TabSwitcher_SelectedTabIndexChanged(System.Object sender, Microsoft.Maui.Controls.SelectedPositionChangedEventArgs e)
    {
        switch (TabSwitcher.SelectedIndex)
        {
            case 0:
                await SetBodyPartsView();
                break;
            case 1:
                await SetWatchView();
                break;
            case 2:
                await SetProfileView();
                break;
        }
    }
    
    private async Task SetBodyPartsView()
    {
        var bodyParts = BodyPartsV.Content as BodyPartsView;
        BodyPartsV.BindingContext = bodyParts;
        await Task.Delay(500);
        if (bodyParts != null)
        {
            await bodyParts.OnViewAppeard();
        }

    }
    private async Task SetWatchView()
    {
        var watch = WatchV.Content as WatchView;
        WatchV.BindingContext = watch;
        await Task.Delay(500);

    }
    private async Task SetProfileView()
    {
        var profile = ProfileV.Content as ProfileView;
        ProfileV.BindingContext = profileVM;
        await Task.Delay(500);
        if (profile != null)
        {
            await profile.OnViewAppeard();
        }

    }
    private async Task CreateUser()
    {
        userDB.InitializeAsync(SQLiteDataAccessPath);
        var users = await userDB.GetAllAsync();
        if(users is null || users.Count == 0)
        {
            User newUser = new User{ Image = "",Weight=72,Height=174,BMI=24};
            await userDB.SaveAsync(newUser);
        }
    }
    private async Task CreateBodyPartsMesures()
    {
        bodyPartsDB.InitializeAsync(SQLiteDataAccessPath);
        var bodyParts = await bodyPartsDB.GetAllAsync();
        if (bodyParts == null || bodyParts.Count == 0)
        {
            var defaultBodyParts = new List<BodyParts>
            {
                new BodyParts { Name = "WEIGHT",        Icon = "balance",       Mesure = 74 },
                new BodyParts { Name = "HEIGHT",        Icon = "straighten",    Mesure = 58 },
                new BodyParts { Name = "BMI",           Icon = "speed",         Mesure = 26.4 },
                new BodyParts { Name = "Neck",          Icon = "neck",          Mesure = 58 },
                new BodyParts { Name = "Shoulder",      Icon = "back",          Mesure = 146 },
                new BodyParts { Name = "Chest",         Icon = "chest",         Mesure = 106 },
                new BodyParts { Name = "Right Biceps",  Icon = "biceps",        Mesure = 42 },
                new BodyParts { Name = "Left Biceps",   Icon = "biceps",        Mesure = 40 },
                new BodyParts { Name = "Right Forearm", Icon = "forearm",       Mesure = 32 },
                new BodyParts { Name = "Left Forearm",  Icon = "forearm",       Mesure = 32 },
                new BodyParts { Name = "Waist",         Icon = "waist",         Mesure = 186 },
                new BodyParts { Name = "Left Calves",   Icon = "calves",        Mesure = 76 },
                new BodyParts { Name = "Right Calves",  Icon = "calves",        Mesure = 76 },
                new BodyParts { Name = "Left Thighs",   Icon = "thighs",        Mesure = 106 },
                new BodyParts { Name = "Right Thighs",  Icon = "thighs",        Mesure = 106 },
            };

            foreach (var bodyPart in defaultBodyParts)
            {
                await bodyPartsDB.SaveAsync(bodyPart);
            }
        }
    }
}
