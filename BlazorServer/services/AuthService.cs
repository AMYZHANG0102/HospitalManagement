/*Hira Ahmad
Summary: AuthService represents the service for handling authentication operations.
*/
using System.Net.Http.Json;
using HospitalManagement.BlazorServer.Models;
namespace HospitalManagement.BlazorServer.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly IJWTService _jwtService;

    public AuthService(HttpClient http, IJWTService jwtService)
    {
        _http = http;
        _jwtService = jwtService;
    }

    public async Task<string> RegisterAsync(RegisterModel request)
    {
        var response = await _http.PostAsJsonAsync("api/patients", request);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return "Registration failed!";
        }

        return "Success";
    }

    public async Task<string> LoginAsync(UserLoginModel request)
    {
        var response = await _http.PostAsJsonAsync("api/auth/login", request);

        if (!response.IsSuccessStatusCode)
        {
            return "Login failed!";
        }

        var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        if (result == null || !result.Success)
            return "Login failed!";
            
        _jwtService.SetToken(result.Token);
        
        return "Success";
    }
}
