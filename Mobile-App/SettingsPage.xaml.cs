using System.Globalization;

namespace Mobile_App;

public partial class SettingsPage : ContentPage
{
    UserController UserController;
    private readonly string defaultLanguageKey = "DefaultLanguage";

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

        // Set default language to English if not already set
        if (!Preferences.ContainsKey(defaultLanguageKey))
        {
            Preferences.Set(defaultLanguageKey, "en");
        }


        var user = UserController.CurrentUser;
        if (user != null)
        {
            var name = user.FirstName + " " + user.LastName;
            welcomeLabel.Text = "Welcome, " + name;
        }

        // Restore language selection
        string selectedLanguage = Preferences.Get(defaultLanguageKey, "en");

        if (selectedLanguage == "en")
        {
            ENcheckbox.IsChecked = true;
            NLcheckbox.IsChecked = false;
        }
        else if (selectedLanguage == "nl")
        {
            ENcheckbox.IsChecked = false;
            NLcheckbox.IsChecked = true;
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
                // Save language selection
                Preferences.Set(defaultLanguageKey, "nl");
            }
        }
        else if (checkBox == ENcheckbox)
        {
            if (ENcheckbox.IsChecked == true)
            {
                NLcheckbox.IsChecked = false;
                LocalizationResourceManager.Instance.SetCulture(new CultureInfo("en"));
                // Save language selection
                Preferences.Set(defaultLanguageKey, "en");
            }
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        button.IsEnabled = false;

        // Call the logout function from the UserController or related service
        await UserController.Logout();

        await Navigation.PopToRootAsync(); // Return to the root page after successful logout
       
        
        button.IsEnabled = true;
    }
}