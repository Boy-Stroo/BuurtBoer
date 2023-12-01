namespace Mobile_App;


public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
	}

	public async Task<List<User>> GetUsersAsync()
	{
        UserService http = new();
        List<User> Users = await http.GetAll();
		return Users;
    }
}