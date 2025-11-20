using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using TrainSheet.ViewModel;
using TrainSheet.Service;
using CommunityToolkit.Maui;
using Sharpnado.Tabs;
using Plugin.LocalNotification;
using LiveChartsCore.SkiaSharpView.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;
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
            .UseMauiCommunityToolkit()
            .UseLocalNotification()
            .UseSharpnadoTabs(loggerEnable:false)
            .UseSkiaSharp()
            .UseLiveCharts()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("MaterialIcons-Regular.ttf", "Material");
                fonts.AddFont("bodyparts.ttf", "BodyParts");
                fonts.AddFont("horizon.otf", "Horizon");
                fonts.AddFont("bodypart.ttf", "BodyParts");
                
            }).ConfigureMauiHandlers(handlers =>
            {
                //The handler will only be called if the target platform is iOS
#if IOS
                handlers.AddHandler<Entry, TrainSheet.Platforms.iOS.EntryHandler>();
#endif
            });
        builder.Services.AddSingleton<MuscleDetailsVM>();
        builder.Services.AddSingleton<TimerBoxViewModel>();
        builder.Services.AddSingleton<ProfileVM>();
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
