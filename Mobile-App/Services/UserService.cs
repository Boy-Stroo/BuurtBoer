using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.VisualBasic;
using Mobile_App;

// This is the service that will be used to make calls to the backend
public class UserService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;
    //Default gateway: 10.0.2.2 (the ip address through which the android emulator can connect to the internet) 
    // Translates to our local pc then port 5077 where backend is running
    string _domain = "http://10.0.2.2:5077";

    // ObservableCollection is useful for data binding, acts like a list
    public ObservableCollection<User> Users { get; private set; }

    public UserService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        Users = new ObservableCollection<User>();
    }

    public async Task GetAll()
    {

        try
        {
            // Call to the backend
            HttpResponseMessage response = await _client.GetAsync($"{_domain}/api/user/all");
            // If the call is successful, read the response and deserialize it into a list of users
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Users = JsonSerializer.Deserialize<ObservableCollection<User>>(content);
            }
        }
        // Too broad of an exception, but yeah
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
    }

    // public async Task GetLogin()

}