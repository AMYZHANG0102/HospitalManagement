using System.Net.Http.Json;
using HospitalManagement.BlazorServer.Models;
namespace HospitalManagement.BlazorServer.Services;

public class AppointmentService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly string _baseUrl;

    public AppointmentService(
        HttpClient httpClient,
        IConfiguration configuration,
        AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _baseUrl = configuration["HospitalManagement:BaseUrl"] ?? "http://localhost:5000/api/";
    }

    public async Task<List<Appointment>> GetShiftsAsync()
    {
        try
        {
            // Check if user is authenticated
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.user;

            // Get user role and ID
            string? role = user.FindFirst("role")?.Value;
            string? userId = user.FindFirst("id")?.Value;

            string url = "";

            switch(role)
            {
                case "Admin":
                    url = $"{_baseUrl}/shifts";
                    break;
                case "Doctor":
                    url = $"{_baseUrl}/doctors/{userId}/shifts";
                    break;
                default:
                    throw new Exception("Unknown role");
            }

            var response = await _httpClient.GetFromJsonAsync<List<Shift>>(url);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: ", ex);
            return null;
        }
    }

    public async Task<Shift?> GetShiftAsync(int id)
    {
        try
        {
            var response = await _httpClient
                                 .GetFromJsonAsync<Shift>($"{_baseUrl}/shifts/{id}");
            return response;                    
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: ", ex);
            return null;
        }
    }

    public async Task<bool> CreateShiftAsync(Shift shift)
    {
        try {
            var response = await _httpClient
                                .PostAsJsonAsync($"{_baseUrl}/shifts", shift);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: ", ex);
            return null;
        }         
    }

    public async Task<bool> UpdateShiftAsync(Shift shift)
    {
        try {
            var response = await _httpClient
                                .PutAsJsonAsync($"{_baseUrl}/shifts/{shift.Id}", shift);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: ", ex);
            return null;
        }         
    }
    
}