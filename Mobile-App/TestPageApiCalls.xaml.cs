using System.Collections.ObjectModel;

namespace Mobile_App;


public partial class TestPageApiCalls : ContentPage
{
    UserService _userService;
    public TestPageApiCalls()
    {
        InitializeComponent();
        _userService = new UserService();
    }

    // This method is called when the page is navigated to.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _userService.GetAll();
        UserView.ItemsSource = _userService.Users;
    }
}