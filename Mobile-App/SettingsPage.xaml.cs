using System.Globalization;

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

    private void ChangeLanguage(object sender, EventArgs e)
    {
        var checkBox = (CheckBox)sender;

        if (checkBox == NLcheckbox)
        {
            if (NLcheckbox.IsChecked == true)
            {
                ENcheckbox.IsChecked = false;
                LocalizationResourceManager.Instance.SetCulture(new CultureInfo("nl"));
            }
        }
        else if (checkBox == ENcheckbox)
        {
            if (ENcheckbox.IsChecked == true)
            {
                NLcheckbox.IsChecked = false;   
                LocalizationResourceManager.Instance.SetCulture(new CultureInfo("en"));
            }
        }

    }
}