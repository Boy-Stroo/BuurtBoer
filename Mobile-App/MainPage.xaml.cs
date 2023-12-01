namespace Mobile_App
{
    public partial class LogInPage : ContentPage
    {
        private bool _english = false;
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
            _english = !_english;
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