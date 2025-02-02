using TrainSheet.View;

namespace TrainSheet;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();


		MainPage = new NavigationPage(new MuscleType());
	}
}
