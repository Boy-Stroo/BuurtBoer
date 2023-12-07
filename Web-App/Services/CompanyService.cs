using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using Web_App;

public class CompanyService : HTTPService
{
    public async Task<ObservableCollection<Company>> GetAll()
    {

        try
        {
            // Call to the backend
            HttpResponseMessage response = await _client.GetAsync($"{_domain}/api/company/all");
            // If the call is successful, read the response and deserialize it into a list of users
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ObservableCollection<Company>>(content);
            }
        }
        // Too broad of an exception, but yeah
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR {ex.Message}");
        }
        return null;
    }
}