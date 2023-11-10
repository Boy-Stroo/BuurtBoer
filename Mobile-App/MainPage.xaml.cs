namespace Mobile_App
{
    public partial class LogInPage : ContentPage
    {
        private bool _english = false;

        public LogInPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.IsEnabled = false;
            await Navigation.PushAsync(new MyPage());
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
            Navigation.PushAsync(new LogInPage());
        }

        private void OnMicrosoftClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPasswordPage());
        }
    }
}