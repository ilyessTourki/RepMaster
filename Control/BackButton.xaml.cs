using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace TrainSheet.Control;

public partial class BackButton : Button
{
	public BackButton()
	{
		InitializeComponent();
	}

    async void BackPreviousPage(System.Object sender, System.EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
