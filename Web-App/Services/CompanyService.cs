using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
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

    public async Task addCompanyDatabase(Company company)
    {
        try
        {
            // Serialize user to JSON
            var json = JsonSerializer.Serialize(company);

            // Create HTTP content from JSON 
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send POST request
            var response = await _client.PostAsync($"{_domain}/api/company/add", content);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR {ex.Message}");
        }
    }

    public async Task DeleteCompaniesDatabase(Guid CompanyId)
    {
        try
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_domain}/api/company/delete/{CompanyId}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"ERROR {ex.Message}");
        }
    }
}