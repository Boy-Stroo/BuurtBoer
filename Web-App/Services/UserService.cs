using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Web_App;
using static System.Runtime.InteropServices.JavaScript.JSType;
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

    public async Task<List<User>> getUsersWithLunches(Guid CompanyID, DayOfWeek dayOfWeek)
    {
        var allEmployees = await GetUsersPerCompany(CompanyID);
        List<User> usersToReturn = new List<User>();
        List<DateTime> daysToCheck = new List<DateTime>();
        DateTime Date = DateTime.Now.Date;

        if ((int)dayOfWeek <= 3)
        {
            if ((int)dayOfWeek == 1)
            {
                daysToCheck.Add(Date.AddDays(7));
                daysToCheck.Add(Date.AddDays(8));
                daysToCheck.Add(Date.AddDays(9));
                daysToCheck.Add(Date.AddDays(10));
                daysToCheck.Add(Date.AddDays(11));
            }
            else if ((int)dayOfWeek == 2)
            {
                daysToCheck.Add(Date.AddDays(6));
                daysToCheck.Add(Date.AddDays(7));
                daysToCheck.Add(Date.AddDays(8));
                daysToCheck.Add(Date.AddDays(9));
                daysToCheck.Add(Date.AddDays(10));
            }
            else if ((int)dayOfWeek == 3)
            {
                daysToCheck.Add(Date.AddDays(5));
                daysToCheck.Add(Date.AddDays(6));
                daysToCheck.Add(Date.AddDays(7));
                daysToCheck.Add(Date.AddDays(8));
                daysToCheck.Add(Date.AddDays(9));
            }
        }
        else if ((int)dayOfWeek > 3)
        {
            if ((int)dayOfWeek == 4)
            {
                daysToCheck.Add(Date.AddDays(11));
                daysToCheck.Add(Date.AddDays(12));
                daysToCheck.Add(Date.AddDays(13));
                daysToCheck.Add(Date.AddDays(14));
                daysToCheck.Add(Date.AddDays(15));
            }
            else if ((int)dayOfWeek == 5)
            {
                daysToCheck.Add(Date.AddDays(10));
                daysToCheck.Add(Date.AddDays(11));
                daysToCheck.Add(Date.AddDays(12));
                daysToCheck.Add(Date.AddDays(13));
                daysToCheck.Add(Date.AddDays(14));
            }
            else if ((int)dayOfWeek == 6)
            {
                daysToCheck.Add(Date.AddDays(9));
                daysToCheck.Add(Date.AddDays(10));
                daysToCheck.Add(Date.AddDays(11));
                daysToCheck.Add(Date.AddDays(12));
                daysToCheck.Add(Date.AddDays(13));
            }
            else if ((int)dayOfWeek == 7)
            {
                daysToCheck.Add(Date.AddDays(8));
                daysToCheck.Add(Date.AddDays(9));
                daysToCheck.Add(Date.AddDays(10));
                daysToCheck.Add(Date.AddDays(11));
                daysToCheck.Add(Date.AddDays(12));
            }
        }
        foreach (var employee in allEmployees)
        {
            bool hasDate = false;
            if (employee.DaysAtOffice != null)
            {
                foreach (var day in daysToCheck)
                {
                    hasDate = employee.DaysAtOffice.Any(d => d.Date.Date == day.Date);
                    if (hasDate == true)
                        break;
                }
            }
            if (hasDate)
                usersToReturn.Add(employee);
        }
        return usersToReturn;
    }

    public async Task<List<User>> getUsersWithoutLunches(Guid CompanyID, DayOfWeek dayOfWeek)
    {
        var allEmployees = await GetUsersPerCompany(CompanyID);
        List<User> usersToReturn = new List<User>();
        List<DateTime> daysToCheck = new List<DateTime>();
        DateTime Date = DateTime.Now.Date;

        if ((int)dayOfWeek <= 3)
        {
            if ((int)dayOfWeek == 1)
            {
                daysToCheck.Add(Date.AddDays(7));
                daysToCheck.Add(Date.AddDays(8));
                daysToCheck.Add(Date.AddDays(9));
                daysToCheck.Add(Date.AddDays(10));
                daysToCheck.Add(Date.AddDays(11));
            }
            else if ((int)dayOfWeek == 2)
            {
                daysToCheck.Add(Date.AddDays(6));
                daysToCheck.Add(Date.AddDays(7));
                daysToCheck.Add(Date.AddDays(8));
                daysToCheck.Add(Date.AddDays(9));
                daysToCheck.Add(Date.AddDays(10));
            }
            else if ((int)dayOfWeek == 3)
            {
                daysToCheck.Add(Date.AddDays(5));
                daysToCheck.Add(Date.AddDays(6));
                daysToCheck.Add(Date.AddDays(7));
                daysToCheck.Add(Date.AddDays(8));
                daysToCheck.Add(Date.AddDays(9));
            }
        }
        else if ((int)dayOfWeek > 3)
        {
            if ((int)dayOfWeek == 4)
            {
                daysToCheck.Add(Date.AddDays(11));
                daysToCheck.Add(Date.AddDays(12));
                daysToCheck.Add(Date.AddDays(13));
                daysToCheck.Add(Date.AddDays(14));
                daysToCheck.Add(Date.AddDays(15));
            }
            else if ((int)dayOfWeek == 5)
            {
                daysToCheck.Add(Date.AddDays(10));
                daysToCheck.Add(Date.AddDays(11));
                daysToCheck.Add(Date.AddDays(12));
                daysToCheck.Add(Date.AddDays(13));
                daysToCheck.Add(Date.AddDays(14));
            }
            else if ((int)dayOfWeek == 6)
            {
                daysToCheck.Add(Date.AddDays(9));
                daysToCheck.Add(Date.AddDays(10));
                daysToCheck.Add(Date.AddDays(11));
                daysToCheck.Add(Date.AddDays(12));
                daysToCheck.Add(Date.AddDays(13));
            }
            else if ((int)dayOfWeek == 7)
            {
                daysToCheck.Add(Date.AddDays(8));
                daysToCheck.Add(Date.AddDays(9));
                daysToCheck.Add(Date.AddDays(10));
                daysToCheck.Add(Date.AddDays(11));
                daysToCheck.Add(Date.AddDays(12));
            }
        }
        foreach (var employee in allEmployees)
        {
            bool hasDate = false;
            if (employee.DaysAtOffice != null)
            {
                foreach (var day in daysToCheck)
                {
                    hasDate = employee.DaysAtOffice.Any(d => d.Date.Date == day.Date);
                    if (hasDate == true)
                        break;
                }
            }
            if (!hasDate)
                usersToReturn.Add(employee);
        } // query maken om met de UserID de Daysatoffice te pakken en als de userid ertussenzit binnen bepaalde datums dan voeg je hem toe aan de lijst.
        return usersToReturn;
    }
}