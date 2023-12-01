using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Microsoft.VisualBasic;
using Mobile_App;

// This is the service that will be used to make calls to the backend
public record UserCredentials(string Email, string Password);
public class UserService : HTTPService
{
    public UserService()
    {

    }

    public async Task<ObservableCollection<User>> GetAll()
    {

        try
        {
            // Call to the backend
            HttpResponseMessage response = await _client.GetAsync($"{_domain}/api/user/all");
            // If the call is successful, read the response and deserialize it into a list of users
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ObservableCollection<User>>(content);
            }
        }
        // Too broad of an exception, but yeah
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR {ex.Message}");
        }
        return null;
    }
    //Post
     public async Task<User> GetLogin(UserCredentials credentials)
    {

            // Call to the backend
            var json = JsonSerializer.Serialize(credentials);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{_domain}/api/user/login", data);
            // If the call is successful, read the response and deserialize it into a users
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<List<User>>(content)[0];
                return user;
            }
        return null;
    }
}