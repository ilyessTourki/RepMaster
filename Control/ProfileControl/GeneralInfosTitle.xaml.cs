namespace TrainSheet.Control.ProfileControl;

public partial class GeneralInfosTitle : StackLayout
{
    public static readonly BindableProperty IconProperty =
            BindableProperty.Create(nameof(Icon), typeof(string), typeof(GeneralInfosTitle), default(string));

    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(GeneralInfosTitle), default(string));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public GeneralInfosTitle()
	{
		InitializeComponent();
    }
}
