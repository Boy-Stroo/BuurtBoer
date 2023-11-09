namespace Mobile_App
{
    public partial class MainPage : ContentPage
    {
        private bool _english = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyPage());
        }

        private void ChangeLanguage(object sender, EventArgs e)
        {
            _english = !_english;
        }
    }
}