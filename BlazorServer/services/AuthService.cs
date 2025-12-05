using System.Net.Http.Json;
using BlazorServer.Models;
namespace BlazorServer.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public AuthService(HttpClient http, IConfiguration configuration)
    {
        _http = http;
        _baseUrl = configuration["HospitalManagementApi:BaseUrl"];
    }

    public async Task<string> RegisterAsync(RegisterModel request)
    {
        var url = $"{_baseUrl}/api/patients";
        var json = System.Text.Json.JsonSerializer.Serialize(request);
        var response = await _http.PostAsJsonAsync(url, request);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return "Registration failed!";
        }

        return "Success";
    }
}
