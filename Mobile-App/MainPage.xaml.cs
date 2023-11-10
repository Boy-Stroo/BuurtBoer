namespace Mobile_App
{
    public partial class MainPage : ContentPage
    {
        private bool _english = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            button.IsEnabled = false;
            Navigation.PushAsync(new MyPage());
            button.IsEnabled = true;
        }

        private void ChangeLanguage(object sender, EventArgs e)
        {
            _english = !_english;
        }

        private void OnLabelClicked(object sender, TappedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}