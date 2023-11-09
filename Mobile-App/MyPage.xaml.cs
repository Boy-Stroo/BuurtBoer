namespace Mobile_App;

public partial class MyPage : ContentPage
{
	public MyPage()
	{
		InitializeComponent();
	}

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}