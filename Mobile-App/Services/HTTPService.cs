using System.Diagnostics;
using System.Text.Json;
using Microsoft.VisualBasic;
using Mobile_App.Models;

public class HTTPService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;
    string _domain = "http://localhost:5077";

    public List<User> Context { get; private set; }

    public HTTPService()
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
        Context = new List<User>();

        Uri uri = new Uri(string.Format(_domain, $"/api/{typeof(User).Name.ToLower()}/all"));
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Context = JsonSerializer.Deserialize<List<User>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return Context;
    }

    // public async Task<List<User>> GetLogin()

}