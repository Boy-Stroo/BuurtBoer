namespace Mobile_App
{
    public partial class LogInPage : ContentPage
    {
        private bool _english = false;

        public LogInPage()
        {
            InitializeComponent();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyPage());
        }

        private void ChangeLanguage(object sender, EventArgs e)
        {
            _english = !_english;
        }

        private void OnLabelClicked(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new ForgotPasswordPage());
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