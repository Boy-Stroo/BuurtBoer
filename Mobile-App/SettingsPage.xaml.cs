namespace Mobile_App;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        button.IsEnabled = false;
        await Navigation.PushAsync(new MyPage());
        button.IsEnabled = true;
    }
}