namespace Mobile_App;

public partial class SettingsPage : ContentPage
{
    UserController UserController;
	public SettingsPage()
	{
		InitializeComponent();
	}
    public SettingsPage(UserController userController)
    {
        InitializeComponent();
        UserController = userController;
    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        button.IsEnabled = false;
        await Navigation.PopAsync();
        button.IsEnabled = true;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var user = UserController.CurrentUser;
        if (user != null)
        {
            var name = user.FirstName + " " + user.LastName;
            welcomeLabel.Text = "Welcome, " + name;
        }
    }
}