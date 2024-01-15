using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Web_App;

namespace Web_App.Services
{
    public class GroceryListService : HTTPService
    {
        public async Task<ObservableCollection<Grocery>> GetAll(Guid CompanyID)
        {

            try
            {
                // Call to the backend
                HttpResponseMessage response = await _client.GetAsync($"{_domain}/api/grocery/all/{CompanyID}");
                // If the call is successful, read the response and deserialize it into a list of users
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ObservableCollection<Grocery>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {ex.Message}");
            }
            return null;
        }

        public async Task addGroceryDatabase(Grocery grocery)
        {
            try
            {
                // Serialize user to JSON
                var json = JsonSerializer.Serialize(grocery);

                // Create HTTP content from JSON 
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send POST request
                var response = await _client.PostAsync($"{_domain}/api/grocery/add", content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {ex.Message}");
            }
        }

        public async Task DeleteGroceryDatabase(Guid GroceryId)
        {
            try
            {
                // delete grocery from database.
                HttpResponseMessage response = await _client.DeleteAsync($"{_domain}/api/grocery/delete/{GroceryId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR {ex.Message}");
            }
        }
    }
}
