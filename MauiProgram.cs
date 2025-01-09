using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using TrainSheet.ViewModel;
using TrainSheet.Service;
#if ANDROID
using Android.Content.Res;
#endif

namespace TrainSheet;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureMopups()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("MaterialIcons-Regular.ttf", "Material");
                fonts.AddFont("horizon.otf", "Horizon");
            }).ConfigureMauiHandlers(handlers =>
            {
                //The handler will only be called if the target platform is iOS
#if IOS
                handlers.AddHandler<Entry, TrainSheet.Platforms.iOS.EntryHandler>();
#endif
            });
        builder.Services.AddSingleton<MuscleDetailsVM>();
        builder.Services.AddSingleton<TimerService>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler,view) => 
		{
#if ANDROID
			handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
			handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
		});
		return builder.Build();
	}
}
