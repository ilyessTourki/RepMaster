using System;
namespace TrainSheet.Utilities
{
	public class ServiceHelper
	{
		private static IServiceProvider _testProvider;

		public static TService GetService<TService>()
			=> (_testProvider ?? Current).GetService<TService>();

		public static IServiceProvider Current =>
#if ANDROID
			MauiApplication.Current.Services;
#elif IOS
	MauiUIApplicationDelegate.Current.Services;
#endif

		public static void SetTestProvider(IServiceProvider provider)
		{
			_testProvider = provider;
		}
	}
}

