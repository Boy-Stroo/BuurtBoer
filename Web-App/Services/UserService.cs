using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Web_App;
using static Web_App.Pages.Employees;

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

    public async Task<ObservableCollection<User>> GetUsersPerCompany(Guid CompanyID)
    {
        try
        {
            // Call to the backend
            var response = await _client.GetAsync($"{_domain}/api/user/all/{CompanyID}");

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
            var user = JsonSerializer.Deserialize<List<User>>(content)![0];
            return user;
        }
        return null;
    }

    public async Task DeleteUsersDatabase(Guid UserId)
    {
        try
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_domain}/api/user/delete/{UserId}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR {ex.Message}");
        }
    }

    public async Task addUserDatabase(User user)
    {
        try
        {
            // Serialize user to JSON
            var json = JsonSerializer.Serialize(user);

            // Create HTTP content from JSON 
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send POST request
            var response = await _client.PostAsync($"{_domain}/api/user/add", content);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR {ex.Message}");
        }
    }
}