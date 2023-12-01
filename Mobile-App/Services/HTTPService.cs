using System.Diagnostics;
using System.Text.Json;
using Microsoft.VisualBasic;
using Mobile_App;


public class UserService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;
    string _domain = "http://localhost:5077";

    public List<User> Users { get; private set; }

    public UserService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<List<User>> GetAll()
    {
        Users = new List<User>();

        Uri uri = new Uri(string.Format(_domain, $"/api/user/all"));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Users = JsonSerializer.Deserialize<List<User>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return Users;
    }

    // public async Task<List<User>> GetLogin()

}