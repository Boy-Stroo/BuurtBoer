using Mobile_App.Resources.Languages;
using System.Globalization;

namespace Mobile_App
{
    public partial class LogInPage : ContentPage
    {
        //private bool _english = false;
        private readonly UserController _userController;
        public LogInPage(UserController userController)
        {
            InitializeComponent();
            _userController = userController;
        }

        public LogInPage()
        {
            InitializeComponent();
            _userController = new UserController(new UserService());
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.IsEnabled = false;
            var userCredentials = new UserCredentials(emailEntry.Text, passwordEntry.Text);
            var success = await _userController.LogIn(userCredentials);

            if (success)
                await Navigation.PushAsync(new HomePage(_userController));

            else
                await DisplayAlert("Error", "Invalid email or password", "OK");

            button.IsEnabled = true;
        }

        private void ChangeLanguage(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (button == NLbutton)
            {
                ENbutton.BackgroundColor = Color.FromArgb("00A89B");
                ENbutton.TextColor = Color.FromArgb("F5F5F5");
                NLbutton.BackgroundColor = Color.FromArgb("E9E9E9");
                NLbutton.TextColor = Color.FromArgb("00A89B");
                LocalizationResourceManager.Instance.SetCulture(new CultureInfo("nl"));
            }
            else if (button == ENbutton)
            {
                ENbutton.BackgroundColor = Color.FromArgb("E9E9E9");
                ENbutton.TextColor = Color.FromArgb("00A89B");
                NLbutton.BackgroundColor = Color.FromArgb("00A89B");
                NLbutton.TextColor = Color.FromArgb("F5F5F5");
                LocalizationResourceManager.Instance.SetCulture(new CultureInfo("en"));
            }

        }

        private async void OnLabelClicked(object sender, TappedEventArgs e)
        {
            Label label = (Label)sender;
            label.IsEnabled = false;
            await Navigation.PushAsync(new ForgotPasswordPage());
            label.IsEnabled = true;
        }

        private void OnGoogleClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TestPageApiCalls(_userController));
        }

        private void OnMicrosoftClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TestPageApiCalls(_userController));
        }
    }
}