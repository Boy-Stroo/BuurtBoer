using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Microsoft.VisualBasic;
using Mobile_App;

public class OfficeDayService : HTTPService
{
    public OfficeDayService()
    { 

    }
    public async Task<ObservableCollection<OfficeDay>> GetAllOfficeDays()
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync($"{_domain}/api/officedays");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ObservableCollection<OfficeDay>>(content);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR {ex.Message}");
        }
        return null;
    }

    public async Task<bool> CreateOfficeDay(DateOnly date, Guid userId)
    {
         
        var officeDay = new OfficeDay
        {
            Date = date,
            UserId = userId
        };

        var json = JsonSerializer.Serialize(officeDay);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"{_domain}/api/officedays", data);

        return response.IsSuccessStatusCode;
        
    }

    public async Task<bool> DeleteOfficeDay(Guid officeDayId)
    {
        try
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_domain}/api/officedays/{officeDayId}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR {ex.Message}");
            return false;
        }
    }
}

