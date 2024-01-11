using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Web_App;
using Web_App.Pages;
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
            if (user.Role != Role.Employee)
            {
                return user;
            }
        }
        return null;
    }

    public async Task DeleteUsersDatabase(Guid UserId)
    {
        try
        {
            //call to backend to delete user from database.
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

    public async Task<List<User>> getUsersWithLunches(Guid CompanyID, DayOfWeek dayOfWeek)// functie om alle users die WEL hun lunches hebben ingevuld te pakken.
    {
        var allEmployees = await GetUsersPerCompany(CompanyID);
        List<User> usersToReturn = new List<User>();
        List<string> daysToCheck = new List<string>();
        DateTime Date = DateTime.Now.Date;

        if ((int)dayOfWeek <= 3) // datums berekenen om te kijken of medewerkers lunches al hebben opgegeven.
        {
            if ((int)dayOfWeek == 1)
            {
                daysToCheck.Add(Date.AddDays(7).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(8).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(9).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(10).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(11).ToString("yyyy-MM-dd"));
            }
            else if ((int)dayOfWeek == 2)
            {
                daysToCheck.Add(Date.AddDays(6).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(7).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(8).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(9).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(10).ToString("yyyy-MM-dd"));
            }
            else if ((int)dayOfWeek == 3)
            {
                daysToCheck.Add(Date.AddDays(5).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(6).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(7).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(8).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(9).ToString("yyyy-MM-dd"));
            }
        }
        else if ((int)dayOfWeek > 3)
        {
            if ((int)dayOfWeek == 4)
            {
                daysToCheck.Add(Date.AddDays(11).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(12).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(13).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(14).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(15).ToString("yyyy-MM-dd"));
            }
            else if ((int)dayOfWeek == 5)
            {
                daysToCheck.Add(Date.AddDays(10).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(11).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(12).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(13).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(14).ToString("yyyy-MM-dd"));
            }
            else if ((int)dayOfWeek == 6)
            {
                daysToCheck.Add(Date.AddDays(9).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(10).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(11).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(12).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(13).ToString("yyyy-MM-dd"));
            }
            else if ((int)dayOfWeek == 7)
            {
                daysToCheck.Add(Date.AddDays(8).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(9).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(10).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(11).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(12).ToString("yyyy-MM-dd"));
            }
        }
        foreach (var date in daysToCheck)
        {
            // alle officedays opvragen met een specifieke datum.
            var response = await _client.GetAsync($"{_domain}/api/officedays/bydate?date={date}");
            var officeDays = await response.Content.ReadFromJsonAsync<List<OfficeDay>>();
            foreach (var employee in allEmployees)
            {
                if (usersToReturn.Contains(employee))
                {
                    continue;
                }
                else
                {
                    // kijken of binnen deze officedays het employee id zit van die employee.
                    if (officeDays.Any(d => d.UserId == employee.Id))
                    {
                        usersToReturn.Add(employee);
                    }
                }
            }
        }
        return usersToReturn;
    }

    public async Task<List<User>> getUsersWithoutLunches(Guid CompanyID, DayOfWeek dayOfWeek)// functie om alle users die NIET hun lunches hebben ingevuld te pakken.
    {
        var allEmployees = await GetUsersPerCompany(CompanyID);
        List<User> usersToReturn = new List<User>();
        List<string> daysToCheck = new List<string>();
        DateTime Date = DateTime.Now.Date;

        if ((int)dayOfWeek <= 3) // datums berekenen om te kijken of medewerkers lunches al hebben ongegeven.
        {
            if ((int)dayOfWeek == 1)
            {
                daysToCheck.Add(Date.AddDays(7).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(8).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(9).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(10).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(11).ToString("yyyy-MM-dd"));
            }
            else if ((int)dayOfWeek == 2)
            {
                daysToCheck.Add(Date.AddDays(6).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(7).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(8).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(9).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(10).ToString("yyyy-MM-dd"));
            }
            else if ((int)dayOfWeek == 3)
            {
                daysToCheck.Add(Date.AddDays(5).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(6).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(7).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(8).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(9).ToString("yyyy-MM-dd"));
            }
        }
        else if ((int)dayOfWeek > 3)
        {
            if ((int)dayOfWeek == 4)
            {
                daysToCheck.Add(Date.AddDays(11).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(12).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(13).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(14).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(15).ToString("yyyy-MM-dd"));
            }
            else if ((int)dayOfWeek == 5)
            {
                daysToCheck.Add(Date.AddDays(10).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(11).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(12).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(13).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(14).ToString("yyyy-MM-dd"));
            }
            else if ((int)dayOfWeek == 6)
            {
                daysToCheck.Add(Date.AddDays(9).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(10).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(11).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(12).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(13).ToString("yyyy-MM-dd"));
            }
            else if ((int)dayOfWeek == 7)
            {
                daysToCheck.Add(Date.AddDays(8).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(9).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(10).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(11).ToString("yyyy-MM-dd"));
                daysToCheck.Add(Date.AddDays(12).ToString("yyyy-MM-dd"));
            }
        }
        foreach (var employee in allEmployees)
        {
            bool hasLunch = false;

            foreach (var date in daysToCheck)
            {
                // alle officedays opvragen met een specifieke datum.
                var response = await _client.GetAsync($"{_domain}/api/officedays/bydate?date={date}");
                var officeDays = await response.Content.ReadFromJsonAsync<List<OfficeDay>>();

                // kijken of binnen deze officedays het employee id zit van die employee.
                if (officeDays.Any(d => d.UserId == employee.Id))
                {
                    hasLunch = true;
                    break;
                }
            }
            // Als hasLunch of false staat voeg je de employee toe aan de lijst.
            if (!hasLunch)
            {
                usersToReturn.Add(employee);
            }
        }
        return usersToReturn;
    }

    public async Task<int> getLunchesCount(Guid EmployeeID)// functie om het aantal lunches van afgelopen maand te pakken voor één employee.
    {
        var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);
        var daysInMonth = DateTime.DaysInMonth(oneMonthAgo.Year, oneMonthAgo.Month);
        var firstDayMonth = new DateTime(oneMonthAgo.Year, oneMonthAgo.Month, 1);
        var lastDayMonth = new DateTime(oneMonthAgo.Year, oneMonthAgo.Month, daysInMonth);

        try
        {
            // Call to the backend
            var response = await _client.GetAsync($"{_domain}/api/officedays");
            var officeDays = await response.Content.ReadFromJsonAsync<List<OfficeDay>>();// alle officedays ophalen.

            int lunchCount = 0;
            if (officeDays != null)
            {
                foreach (var day in officeDays)
                {
                    if (day.Date >= firstDayMonth && day.Date <= lastDayMonth)// als de office(day) binnen de afgelopen maand zit dan lunchCount+1;
                    {
                        if (day.UserId == EmployeeID)
                        {
                            lunchCount++;
                        }
                    }
                }
                return lunchCount;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR {ex.Message}");
        }
        return default;
    }
}