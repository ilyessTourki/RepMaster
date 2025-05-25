using static Java.Nio.Channels.FileChannel;

namespace TrainSheet.View;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = this;
    }
    protected override async void OnAppearing()
    {
        TabSwitcher.SelectedIndex = 0;

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
        //if (bodyParts != null)
        //{
        //    await homeView.OnViewAppeard();
        //    homePageContentView = homeView;
        //}

    }
    private async Task SetProfileView()
    {
        //HomeView.BindingContext = vmHome;
        await Task.Delay(500);
        var profile = ProfileV.Content as ProfileView;
        //if (bodyParts != null)
        //{
        //    await homeView.OnViewAppeard();
        //    homePageContentView = homeView;
        //}

    }
}
