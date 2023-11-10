namespace Mobile_App;

public partial class MyPage : ContentPage
{
    public MyPage()
    {
        InitializeComponent();
    }

    private void PreviousWeekButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void NextWeekButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
    private async void Button_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }


}