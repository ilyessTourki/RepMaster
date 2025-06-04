using System.Windows.Input;

namespace TrainSheet.Control.ProfileControl;

public partial class GeneralInfoControl : StackLayout
{
    public bool isEditingUser { get; set; }
    public string editUserIcon { get; set; }
    public ICommand editUserInfo { get; }

    public GeneralInfoControl()
	{
		InitializeComponent();
        isEditingUser = false;
        editUserIcon = "edit";
        editUserInfo = new Command(EditUserInfo);
        BindingContext = this;
    }
    private void EditUserInfo()
    {
        isEditingUser = !isEditingUser;
        OnPropertyChanged(nameof(isEditingUser));
        editUserIcon = isEditingUser ? "check" : "edit";
        OnPropertyChanged(nameof(editUserIcon));
    }
}
