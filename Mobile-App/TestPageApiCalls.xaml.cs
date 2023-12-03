using System.Collections.ObjectModel;

namespace Mobile_App;


public partial class TestPageApiCalls : ContentPage
{
    UserController _userController;
    public TestPageApiCalls(UserController userController)
    {
        InitializeComponent();
        _userController = userController;
    }
    public TestPageApiCalls()
    {
        InitializeComponent();
    }

    // This method is called when the page is navigated to.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _userController.GetAllUsers();
        UserView.ItemsSource = _userController.Users;
    }
}